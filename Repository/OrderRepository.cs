using GroceryAPI.Data;
using GroceryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> PlaceOrder(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
                return null;

            var items = cart.Items.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Product.Price
            }).ToList();

            var total = items.Sum(x => x.Price * x.Quantity);

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                TotalAmount = total,
                Status = "Pending", // ✅ IMPORTANT
                Items = items
            };

            _context.Orders.Add(order);

            // Clear cart after order
            _context.CartItems.RemoveRange(cart.Items);

            await _context.SaveChangesAsync();

            return order;
        }
        // 1. My Orders
        public async Task<List<Order>> GetOrdersByUser(string userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        // 2. All Orders
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();
        }

        // 3. Update Status
        public async Task<bool> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            order.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}