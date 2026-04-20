using AutoMapper;
using GroceryAPI.Data;
using GroceryAPI.DTOs;
using GroceryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product?> AddProduct(CreateProductDto dtos)
        {
            var product = _mapper.Map<Product>(dtos);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.Id);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GetProductDtos>> GetAllProduct()
        {
            var products =  await _context.Products.Include( x => x.Category).ToListAsync();
            return _mapper.Map<IEnumerable<GetProductDtos>>(products);
        }

        public async Task<GetProductDtos?> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return null;

            return _mapper.Map<GetProductDtos>(product);
        }

        public async Task<Product?> UpdateProduct(int id, UpdateProductDto dtos)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;
            _mapper.Map(dtos,product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
