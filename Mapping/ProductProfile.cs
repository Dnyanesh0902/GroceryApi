using AutoMapper;
using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, GetProductDtos>().ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CreateProductDto, Product>();
        }
    }
}
