using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Repository;

namespace GroceryAPI.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repo;
        public ProductServices(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product?> AddProduct(CreateProductDto dto)
        {
            return await _repo.AddProduct(dto);
        }

        public async Task<bool> DeleteProduct(int id)
        {
           return await _repo.DeleteProduct(id);
        }

        public async Task<IEnumerable<GetProductDtos>> GetAllProduct()
        {
            return await _repo.GetAllProduct();
        }

        public async Task<GetProductDtos?> GetProductById(int id)
        {
            return await _repo.GetProductById(id);
        }

        public async Task<Product?> UpdateProduct(int id, UpdateProductDto dto)
        {
            return await _repo.UpdateProduct(id, dto);  
        }
    }
}
