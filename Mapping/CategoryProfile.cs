using AutoMapper;
using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        { 
            CreateMap<Category, GetCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
