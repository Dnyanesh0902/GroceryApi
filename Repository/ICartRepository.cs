using GroceryAPI.Models;

namespace GroceryAPI.Repository
{
    public interface ICartRepository
    {
        Task AddToCart(string userId, int productId, int qty);

        Task<Cart?> GetCart(string userId);
    }
}
