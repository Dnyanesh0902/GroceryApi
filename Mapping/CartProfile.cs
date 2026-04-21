using AutoMapper;
using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, CartItemDto>()
    .ForMember(dest => dest.ProductName,
        opt => opt.MapFrom(src => src.Product.Name))
    .ForMember(dest => dest.Price,
        opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}
