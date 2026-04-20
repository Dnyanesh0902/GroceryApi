using GroceryAPI.DTOs;
using GroceryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;
        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _services.GetAllCategories();
            return Ok(categories);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dtos)
        {
            var category = await _services.CreateCategory(dtos);
            var result = new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
            return Ok(result);
        }
    }
}
