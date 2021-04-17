using DaraAds.Application.Services.Category.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet("getchild/{parentCategoryId}")]
        public async Task<IActionResult> GetChildCategories(int parentCategoryId, CancellationToken cancellationToken)
        {
            var result = await _categorySerivce.GetChildCategories(new GetChildCategories.Request
            {
                ParentCategoryId = parentCategoryId
            }, cancellationToken);

            return Ok(result);
        }
    }
}
