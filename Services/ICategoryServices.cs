using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Services
{
    public interface ICategoryServices
    {
        Task<IEnumerable<GetCategoryDto>> GetAllCategories();
        Task<Category?> CreateCategory(CreateCategoryDto dto);
    }
}
