using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace DaraAds.API.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet("get/top")]
        public async Task<IActionResult> GetTopCategories(CancellationToken cancellationToken)
        {
            var result = await _categorySerivce.GetTopCategories(cancellationToken);
            return Ok(result);
        }
    }
}
