using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductDtos>> GetAllProduct();
        Task<GetProductDtos?> GetProductById(int id);
        Task<Product?> AddProduct(CreateProductDto dtos);
        Task<Product?> UpdateProduct(int id, UpdateProductDto dtos);
        Task<bool> DeleteProduct(int id);
    }
}
