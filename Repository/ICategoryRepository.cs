using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<Category?> AddCategory(CreateCategoryDto dto);
        Task<IEnumerable<GetCategoryDto>> GetAllCategories();
    }
}
