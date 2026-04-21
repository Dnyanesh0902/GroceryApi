using GroceryAPI.Data;
using GroceryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCart(string userId, int productId, int qty)
        {
            var cart = await _context.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            // Create cart if not exists
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
            }

            // Check if product already exists in cart
            var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                item.Quantity += qty;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = qty
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Cart?> GetCart(string userId)
        {
            return await _context.Carts
                .Include(x => x.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}