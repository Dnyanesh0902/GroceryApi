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

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                // ✅ STOCK CHECK
                if (item.Product.Stock < item.Quantity)
                    throw new Exception($"Not enough stock for {item.Product.Name}");

                // ✅ STOCK DEDUCTION
                item.Product.Stock -= item.Quantity;

                total += item.Product.Price * item.Quantity;

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.Now,
                TotalAmount = total,
                Status = "Pending",
                Items = orderItems
            };

            _context.Orders.Add(order);

            // ✅ CLEAR CART AFTER ORDER
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
            // ✅ VALIDATION HERE
            var validStatuses = new[] { "Pending", "Shipped", "Delivered", "Cancelled" };

            if (!validStatuses.Contains(status))
                return false;

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return false;

            order.Status = status;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Order?> GetById(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<bool> CancelOrder(int orderId, string userId, bool isAdmin)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            // ✅ USER OWNERSHIP CHECK (SKIP FOR ADMIN)
            if (!isAdmin && order.UserId != userId)
                return false;

            // ❌ Cannot cancel delivered order (even admin should avoid normally, but we allow override)
            if (!isAdmin && order.Status == "Delivered")
                return false;

            // ❌ Already cancelled
            if (order.Status == "Cancelled")
                return false;

            // ⏰ TIME LIMIT (only for normal user)
            if (!isAdmin && order.CreatedAt < DateTime.Now.AddHours(-2))
                return false;

            // 🔁 Restore stock
            foreach (var item in order.Items)
            {
                item.Product.Stock += item.Quantity;
            }

            order.Status = "Cancelled";

            await _context.SaveChangesAsync();

            return true;
        }
    }
}