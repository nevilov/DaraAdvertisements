﻿using DaraAds.Application.Common;
using DaraAds.Application.Identity.Contracts.Exceptions;
using DaraAds.Application.Identity.Interfaces;
using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Advertisement.Contracts;
using DaraAds.Application.Services.Advertisement.Contracts.Exceptions;
using DaraAds.Application.Services.Advertisement.Interfaces;
using DaraAds.Application.Services.Category.Contracts.Exceptions;
using DaraAds.Application.Services.Image.Contracts;
using DaraAds.Application.Services.Image.Interfaces;
using DaraAds.Application.Services.S3.Contracts.Exceptions;
using DaraAds.Application.Services.S3.Interfaces;
using DaraAds.Application.Services.User.Contracts.Exceptions;
using ExcelDataReader;
using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static DaraAds.Application.Services.Advertisement.Contracts.GetPages.Response;
using Delete = DaraAds.Application.Services.Advertisement.Contracts.Delete;
using DeleteImage = DaraAds.Application.Services.Advertisement.Contracts.DeleteImage;
using Get = DaraAds.Application.Services.Advertisement.Contracts.Get;

namespace DaraAds.Application.Services.Advertisement.Implementations
{
    public sealed class AdvertisementService : IAdvertisementService
    {
        private readonly Repositories.IAdvertisementRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IRepository<Domain.Image, string> _imageRepository;
        private readonly IRepository<Domain.User, string> _userRepository;
        private readonly IImageService _imageService;
        private readonly IS3Service _s3Service;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public AdvertisementService(
            IAdvertisementRepository repository,
            IIdentityService identityService,
            IImageService imageService,
            IRepository<Domain.Image, string> imageRepository,
            IS3Service s3Service,
            ICategoryRepository categoryRepository,
            ISendEndpointProvider sendEndpointProvider,
            IRepository<Domain.User, string> userRepository)
        {
            _repository = repository;
            _identityService = identityService;
            _imageService = imageService;
            _imageRepository = imageRepository;
            _s3Service = s3Service;
            _categoryRepository = categoryRepository;
            _sendEndpointProvider = sendEndpointProvider;
            _userRepository = userRepository;
        }

        private const string S3Url = "https://storage.yandexcloud.net/dara-ads-images/";

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserForAdCreationException($"Попытка создания объявления [{request.Title}] без пользователя.");
            }

            var ad = new Domain.Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                Cover = request.Cover,
                OwnerId = userId,
                Status = Domain.Advertisement.Statuses.Created,
                CreatedDate = DateTime.UtcNow,
                CategoryId = request.CategoryId
            };

            await _repository.Save(ad, cancellationToken);
            return new Create.Response
            {
                Id = ad.Id
            };
        }
        public async Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);
            
            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            if(ad.Status == Domain.Advertisement.Statuses.Closed)
            {
                throw new AdNotFoundException(request.Id);
            }

            return new Get.Response
            {
                Title = ad.Title,
                Description = ad.Description,
                Status = ad.Status.ToString(),
                Price = ad.Price,
                Cover = ad.Cover,
                CreatedDate = ad.CreatedDate,
                Images = ad.Images.Select(i => new Get.Response.ImageResponse
                {
                    Id = i.Id,
                    ImageUrl =  S3Url + i.Name
                }),

                Category = new Get.Response.CategoryResponse
                {
                    Id = ad.Category.Id,
                    Name = ad.Category.Name
                },

                Owner = new Get.Response.OwnerResponse
                {
                    Id = ad.OwnerUser.Id,
                    Username = ad.OwnerUser.Username,
                    Name = ad.OwnerUser.Name,
                    Phone = ad.OwnerUser.Phone,
                    Lastname = ad.OwnerUser.LastName,
                    Avatar = ad.OwnerUser.Avatar
                }
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var ad = await _repository.FindById(request.Id, cancellationToken);

            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (ad.Status != Domain.Advertisement.Statuses.Created)
            {
                throw new AdShouldBeInCreatedStateForClosingException(ad.Id);
            }

            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && ad.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            ad.Status = Domain.Advertisement.Statuses.Closed;
            ad.UpdatedDate = DateTime.UtcNow;
            ad.RemovedDate = DateTime.UtcNow;

            await _repository.Save(ad, cancellationToken);
        }

        public async Task<GetPages.Response> GetPages(GetPages.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetPages.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var ads = await _repository.GetPageByFilterSortSearch(request, cancellationToken);

            return new GetPages.Response
            {
                Items = ads.Select(a => new Item
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Cover = a.Cover,
                    Price = a.Price,
                    CreatedDate = a.CreatedDate,
                    Status = a.Status.ToString(),

                    Owner = new OwnerResponse
                    {
                        Id = a.OwnerId,
                        Username = a.OwnerUser.Username,
                        Email = a.OwnerUser.Email,
                        Name = a.OwnerUser.Name,
                        Lastname = a.OwnerUser.LastName,
                        Avatar = a.OwnerUser.Avatar
                    },

                    Images = a.Images.Select(i => new ImageResponse
                    {
                        Id = i.Id,
                        ImageUrl =  S3Url + i.Name
                    }),
                }),
                Total = ads.Total,
                Offset = request.Offset,
                Limit = request.Limit,
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var advertisement = await _repository.FindById(request.Id, cancellationToken);

            if (advertisement == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (advertisement.Status == Domain.Advertisement.Statuses.Closed)
            {
                throw new AdNotFoundException(request.Id);
            }

            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && advertisement.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            advertisement.Title = request.Title;
            advertisement.Description = request.Description;
            advertisement.Price = request.Price;
            advertisement.Cover = request.Cover;
            advertisement.UpdatedDate = DateTime.UtcNow;
            advertisement.CategoryId = request.CategoryId;

            await _repository.Save(advertisement, cancellationToken);

            return new Update.Response
            {
                Id = request.Id
            };
        }

        public async Task<GetPagedByCategory.Response> GetPagedByCategory(GetPagedByCategory.Request request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.FindById(request.CategoryId, cancellationToken);
            if(category == null)
            {
                throw new NoCategoryFoundException($"Категория с id {request.CategoryId} не была найдена");
            }

            var categoryIds = await _categoryRepository.FindCategoryIdsByParent(request.CategoryId, cancellationToken);

            var advertisementsByCategories = await _repository.FindAdvertisementsByCategoryIds(categoryIds, request.Limit, request.Offset, cancellationToken);

            var total = advertisementsByCategories.Count();
            if (total == 0)
            {
                return new GetPagedByCategory.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            return new Contracts.GetPagedByCategory.Response 
            {
                Items = advertisementsByCategories.Select(a => new GetPagedByCategory.Response.Item
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Cover = a.Cover,
                    Price = a.Price,
                    CreatedDate = a.CreatedDate,
                    Status = a.Status.ToString(),

                    Owner = new GetPagedByCategory.Response.OwnerResponse
                    {
                        Id = a.OwnerId,
                        Username = a.OwnerUser.Username,
                        Email = a.OwnerUser.Email,
                        Name = a.OwnerUser.Name,
                        Lastname = a.OwnerUser.LastName,
                        Images = a.OwnerUser.Images.Select(i => new GetPagedByCategory.Response.ImageResponse
                        {
                            Id = i.Id,
                            ImageUrl = S3Url + i.Name
                        })
                    },

                    Images = a.Images.Select(i => new GetPagedByCategory.Response.ImageResponse
                    {
                        Id = i.Id,
                        ImageUrl = S3Url + i.Name
                    }),
                }),
                Total = advertisementsByCategories.Count(),
                Offset = request.Offset,
                Limit = request.Limit,
            };
        }

        public async Task<Search.Response> Search(Search.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new Search.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var result = await _repository.Search(x => x.Title.Contains(request.KeyWord)
            || x.Description.Contains(request.KeyWord), request.Limit, request.Offset, cancellationToken);

            return new Search.Response
            {
                Items = result.Select(a => new Search.Response.Item
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Cover = a.Cover,
                    Price = a.Price,
                    Status = a.Status.ToString(),
                    Images = a.Images.Select(i => new Search.Response.ImageResponse
                    {
                        Id = i.Id,
                        ImageUrl =  S3Url + i.Name
                    })
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }

        public async Task<GetUserAdvertisements.Response> GetUserAdvertisements(GetUserAdvertisements.Request request, CancellationToken cancellationToken)
        {
            string userId;

            if (string.IsNullOrEmpty(request.Id))
            {
                var userIdFromClaims = await _identityService.GetCurrentUserId(cancellationToken);
                if (string.IsNullOrEmpty(userIdFromClaims))
                {
                    throw new NoUserFound("Пользователь не найден");
                }
                userId = userIdFromClaims;
            }
            else
            {
                userId = request.Id;
            }

            var result = await _repository.FindUserAdvertisements(userId, request.Limit, request.Offset, cancellationToken);

            if (result == null)
            {
                throw new NoUserAdFoundException($"Объявления пользователя с Id = {userId} не найдены");
            }

            return new GetUserAdvertisements.Response
            {
                Items = result.Select(a => new GetUserAdvertisements.Response.Item
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Cover = a.Cover,
                    CreatedDate = a.CreatedDate,
                    Price = a.Price,

                    Status = a.Status.ToString()
                }),
                Total = result.Count(),
                Offset = request.Offset,
                Limit = request.Limit
            };
        }

        public async Task AddImage(AddImage.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var advertisement = await _repository.FindById(request.Id, cancellationToken);

            if (advertisement == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (advertisement.Status == Domain.Advertisement.Statuses.Closed)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (advertisement.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            var response = await _imageService.Upload(
                new UploadImage.Request
                {
                    Image = request.Image
                }, cancellationToken);

            var image = await _imageRepository.FindById(response.Id, cancellationToken);

            advertisement.Images.Add(image);

            await _repository.Save(advertisement, cancellationToken);

        }

        public async Task DeleteImage(DeleteImage.Request request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new NoUserFoundException($"Пользователь не найден");
            }

            var advertisement = await _repository.FindById(request.Id, cancellationToken);

            if (advertisement == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (advertisement.Status == Domain.Advertisement.Statuses.Closed)
            {
                throw new AdNotFoundException(request.Id);
            }

            if (advertisement.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            var image = await _imageRepository.FindById(request.ImageId, cancellationToken);

            if (advertisement.Id != image.AdvertisementId)
            {
                throw new DeletingImageException(
                    $"Изображение c id [{image.Id}] не может быть удалено из объявления с Id [{advertisement.Id}].");
            }

            advertisement.Images.Remove(image);

            await _s3Service.DeleteFile(image.Name, cancellationToken);

            await _repository.Save(advertisement, cancellationToken);

        }

        public async Task<GetImageByAdvertisement.Response> GetImageByAdvertisement(GetImageByAdvertisement.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);

            if (ad == null)
            {
                throw new AdNotFoundException(request.Id);
            }

            var advertisementCover = ad.Images.ElementAt(0).Id;

            var image = await _imageRepository.FindById(advertisementCover, cancellationToken);

            if (image == null)
            {
                throw new ImageNotFoundException();
            }

            var imageUrl = $"https://storage.yandexcloud.net/dara-ads-images/{image.Name}";

            return new GetImageByAdvertisement.Response
            {
                ImageUrl = imageUrl,
                ImageBlob = Convert.ToBase64String(image.ImageBlob)
            };
        }

        public async Task ImportExcelProducer(Stream excelFileStream, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var domainUser = await _userRepository.FindById(userId, cancellationToken);

            if (!domainUser.IsCorporation)
            {
                throw new HaveNoRightException($"У пользователя с id {userId} нет прав массово загружать объявелния");
            }

            var importExcelEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:import_excel"));
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(excelFileStream))
            {
                DataSet excelFileData = excelReader.AsDataSet(new ExcelDataSetConfiguration 
                { 
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true}
                });

                DataRowCollection excelRows = excelFileData.Tables[0].Rows;

                foreach(DataRow row in excelRows)
                {
                    var message = new ImportExcelMessage
                    {
                        Title = row.ItemArray[0].ToString(),
                        Description = row.ItemArray[1].ToString(),
                        Price = Convert.ToDecimal(row.ItemArray[2].ToString()),
                        CategoryId = Convert.ToInt32(row.ItemArray[3].ToString()),
                        OwnerId = userId
                    };
                    await importExcelEndpoint.Send(message, cancellationToken);
                }
            }
        }

        public async Task CreateByExcelConsumer(ImportExcelMessage message, CancellationToken cancellationToken)
        {
            var advertisement = new Domain.Advertisement
            {
                Title = message.Title,
                Description = message.Description,
                Price = message.Price,
                OwnerId = message.OwnerId,
                CategoryId = message.CategoryId,
                CreatedDate = DateTime.UtcNow,
                Status = Domain.Advertisement.Statuses.Created
            };
            
            await _repository.Save(advertisement, cancellationToken);
        }
    }
}