using GroceryAPI.DTOs;
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

        // ✅ UPDATED METHOD
        public async Task<GetCartDtos?> GetCart(string userId)
        {
            var cart = await _repo.GetCart(userId);

            // if cart empty
            if (cart == null || !cart.Items.Any())
                return new GetCartDtos
                {
                    Items = new List<CartItemDto>(),
                    TotalAmount = 0
                };

            // map data
            var items = cart.Items.Select(x => new CartItemDto
            {
                ProductName = x.Product.Name,
                Price = x.Product.Price,
                Quantity = x.Quantity
            }).ToList();

            // calculate total
            var total = items.Sum(x => x.Price * x.Quantity);

            return new GetCartDtos
            {
                Items = items,
                TotalAmount = total
            };
        }
    }
}