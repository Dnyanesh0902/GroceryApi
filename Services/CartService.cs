using GroceryAPI.Models;
using GroceryAPI.Repository;

namespace GroceryAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo)
        {
            _repo = repo;
        }

        public async Task AddToCart(string userId, int productId, int qty)
        {
            await _repo.AddToCart(userId, productId, qty);
        }

        public async Task<Cart?> GetCart(string userId)
        {
            return await _repo.GetCart(userId);
        }
    }
}