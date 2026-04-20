using AutoMapper;
using GroceryAPI.Data;
using GroceryAPI.DTOs;
using GroceryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Category> AddCategory(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<GetCategoryDto>> GetAllCategories()
        {
            var category = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<GetCategoryDto>>(category);
        }
    }
}
