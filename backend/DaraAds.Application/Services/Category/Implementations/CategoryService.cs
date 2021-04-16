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

        public CategoryService(ICategoryRepository categoryReposity, IAdvertisementRepository advertisementRepository)
        {
            _categoryReposity = categoryReposity;
        }

        public async Task<GetChildCategories.Response> GetChildCategories(GetChildCategories.Request request, CancellationToken cancellationToken)
        {
            var parentCategory = await _categoryReposity.FindById(request.ParentCategoryId, cancellationToken);
            if (parentCategory == null)
            {
                throw new NoCategoryFoundException($"Категория с id {request.ParentCategoryId} не была найдено");
            }

            var result = await _categoryReposity.FindChildCategories(request.ParentCategoryId, cancellationToken);
            foreach(var res in result)
            {
                System.Console.WriteLine(res.Name);
            }
            return new GetChildCategories.Response
            {
                CategoryItems = result.Select(a => new GetChildCategories.Response.CategoryItem
                {
                    Id = a.Id,
                    Name = a.Name
                })
            };
        }
    }
}
