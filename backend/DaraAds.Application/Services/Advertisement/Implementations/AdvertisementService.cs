using DaraAds.Application.Common;
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
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DaraAds.Application.Helpers;
using static DaraAds.Application.Services.Advertisement.Contracts.GetPages.Response;
using Delete = DaraAds.Application.Services.Advertisement.Contracts.Delete;
using DeleteImage = DaraAds.Application.Services.Advertisement.Contracts.DeleteImage;
using Get = DaraAds.Application.Services.Advertisement.Contracts.Get;

namespace DaraAds.Application.Services.Advertisement.Implementations
{
    public sealed class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _repository;
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
                Location = request.Location,
                GeoLat = request.GeoLat,
                GeoLon = request.GeoLon,
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
                Location = ad.Location,
                GeoLat = ad.GeoLat,
                GeoLon = ad.GeoLon,
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
            
            var isCategorySet = request.CategoryId != 0;

            List<int> categoryIds = null;

            PagedList<Domain.Advertisement> ads;
            if(isCategorySet)
            {
                categoryIds = await _categoryRepository.FindCategoryIdsByParent(request.CategoryId, cancellationToken);
            }
            
            ads = await _repository.GetPageByFilterSortSearch(request, categoryIds, cancellationToken);

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
                    Location = a.Location,

                    Owner = new OwnerResponse
                    {
                        Id = a.OwnerId,
                        Username = a.OwnerUser.Username,
                        Email = a.OwnerUser.Email,
                        Name = a.OwnerUser.Name,
                        Lastname = a.OwnerUser.LastName,
                        Avatar = a.OwnerUser.Avatar
                    },

                    Category = new GetPages.Response.CategoryResponse
                    {
                        Id = a.Category.Id,
                        Name = a.Category.Name
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
            advertisement.Location = request.Location;
            advertisement.GeoLat = request.GeoLat;
            advertisement.GeoLon = request.GeoLon;
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
       

        public async Task<GetUserAdvertisements.Response> GetUserAdvertisements(GetUserAdvertisements.Request request, CancellationToken cancellationToken)
        {
            
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetUserAdvertisements.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }
            
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

            var result = await _repository.FindUserAdvertisements(userId, request.Limit, request.Offset, request.SortBy, request.SortDirection, cancellationToken);

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
                    Location = a.Location,
                    
                    Images = a.Images.Select(i => new GetUserAdvertisements.Response.ImageResponse
                    {
                        Id = i.Id,
                        ImageUrl = S3Url + i.Name
                    }),
                    Category = new GetUserAdvertisements.Response.CategoryResponse
                    {
                        Id = a.Category.Id,
                        Name = a.Category.Name
                    },
                    Status = a.Status.ToString()
                }),
                Total = result.Total,
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


        public async Task ImportExcelProducer(IFormFile excel, CancellationToken cancellationToken)
        {
            if(excel.Length > 10000)
            {
                throw new ImportExcelException("Файл не может быть больше 10 000 байт");
            }
            var supportedExcelFiles = new[] { "xls", "xlsx" };
            var fileExt = System.IO.Path.GetExtension(excel.FileName).Substring(1);
            if (!supportedExcelFiles.Contains(fileExt))
            {
                throw new ImportExcelException("Данный формат файла не поддерживается, загрузите excel файл");
            }

            var excelFileStream = excel.OpenReadStream();

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var domainUser = await _userRepository.FindById(userId, cancellationToken);

            if (!domainUser.IsCorporation)
            {
                throw new HaveNoRightException($"У пользователя с id {userId} нет прав массово загружать объявления");
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
                        Location = Convert.ToString(row.ItemArray[4].ToString()),
                        GetLat = Convert.ToDecimal(row.ItemArray[5].ToString()),
                        GeoLon = Convert.ToDecimal(row.ItemArray[6].ToString()),
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
                Location = message.Location,
                GeoLat = message.GetLat,
                GeoLon = message.GeoLon,
                Status = Domain.Advertisement.Statuses.Created
            };
            
            await _repository.Save(advertisement, cancellationToken);
        }
    }
}