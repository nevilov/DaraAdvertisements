using DaraAds.Application.Services.Category.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DaraAds.API.Controllers.Category
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class CategoryController : ControllerBase
    {
        private ICategoryService _categorySerivce;
        
        public CategoryController(ICategoryService categorySerivce)
        {
            _categorySerivce = categorySerivce;
        }
    }
}
