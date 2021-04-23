using DaraAds.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Category
{
    public partial class CategoryController
    {
        /// <summary>
        /// Получить категорию вместе с дочерними категориями
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var result = await _categorySerivce.GetCategoryById(new GetCategoryById.Request
            {
                ParentCategoryId = id
            }, cancellationToken);

            return Ok(result);
        }
    }
}
