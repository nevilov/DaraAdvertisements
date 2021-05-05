using DaraAds.Application.Repositories;
using DaraAds.Application.Services.Category.Contracts;
using DaraAds.Application.Services.Category.Contracts.Exceptions;
using DaraAds.Application.Services.Category.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.Application.Services.Category.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryReposity;

        public CategoryService(ICategoryRepository categoryReposity)
        {
            _categoryReposity = categoryReposity;
        }

        public async Task<GetCategoryById.Response> GetCategoryById(GetCategoryById.Request request, CancellationToken cancellationToken)
        {
            var parentCategory = await _categoryReposity.FindById(request.ParentCategoryId, cancellationToken);
            if (parentCategory == null)
            {
                throw new NoCategoryFoundException($"Категория с id {request.ParentCategoryId} не была найдено");
            }

            var result = await _categoryReposity.FindById(request.ParentCategoryId, cancellationToken);

            return new GetCategoryById.Response
            {
                Parent = new GetCategoryById.Response.ParentCategoryItem
                {
                    Id = result.Id,
                    Name = result.Name,
                    ChildCategories = result.ChildCategories.Select(a => new GetCategoryById.Response.ChildCategoryItem
                    {
                        Id = a.Id,
                        Name = a.Name
                    })
                }
            };
        }

        public async Task<GetTopCategories.Response> GetTopCategories(CancellationToken cancellationToken)
        {
            var topCategories = await _categoryReposity.FindTopCategories(cancellationToken);

            return new GetTopCategories.Response
            {
                Categories = topCategories.Select(a => new GetTopCategories.Response.TopCategory
                {
                    Id = a.Id,
                    Name = a.Name,
                    ChildCategories = a.ChildCategories.Select(c => new GetTopCategories.Response.ChildCategories
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                })
            };
        }
    }
}
