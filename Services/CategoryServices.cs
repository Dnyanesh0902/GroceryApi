using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Repository;

namespace GroceryAPI.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryServices(ICategoryRepository category)
        {
            _categoryRepository = category;
        }
        public async Task<Category?> CreateCategory(CreateCategoryDto dto)
        {
            return await _categoryRepository.AddCategory(dto);
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllCategories()
        {
           return await _categoryRepository.GetAllCategories();
        }
    }
}
