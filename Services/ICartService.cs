using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Services
{
    public interface ICartService
    {
        Task AddToCart(string userId, int productId, int qty);
        Task<GetCartDtos?> GetCart(string userId);
    }
}