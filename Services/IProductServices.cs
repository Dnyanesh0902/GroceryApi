using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<GetProductDtos>> GetAllProduct();
        Task<GetProductDtos?> GetProductById(int id);
        Task<Product?> AddProduct(CreateProductDto dto);
        Task<Product?> UpdateProduct(int id, UpdateProductDto dto);
        Task<bool> DeleteProduct(int id);
    }
}
