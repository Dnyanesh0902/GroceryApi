using GroceryAPI.Data;
using GroceryAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using GroceryAPI.Models;

namespace GroceryAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment?> MakePayment(string userId, CreatePaymentDto dto)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == dto.OrderId && o.UserId == userId);

            if (order == null || order.Status == "Paid")
                return null;

            // 🔥 Simulate payment gateway
            var random = new Random();
            var isSuccess = random.Next(1, 10) > 2;

            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalAmount,
                Method = dto.Method,
                Status = isSuccess ? "Success" : "Failed",
                PaidAt = DateTime.Now
            };

            if (isSuccess)
            {
                order.Status = "Paid";
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }
        public async Task<bool> RefundOrder(int orderId, string userId, bool isAdmin)
        {
            var order = await _context.Orders
                .Include(o => o.Payment)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            if (!isAdmin && order.UserId != userId)
                return false;

            // ❌ cannot refund if not paid
            if (order.Status != "Paid")
                return false;

            // 🔁 restore stock
            foreach (var item in order.Items)
            {
                item.Product.Stock += item.Quantity;
            }

            order.Status = "Refunded";

            if (order.Payment != null)
                order.Payment.Status = "Refunded";

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<Payment?> RetryPayment(string userId, int orderId)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.OrderId == orderId);

            if (payment == null || payment.Order.UserId != userId)
                return null;

            if (payment.Status == "Success")
                return null;

            var random = new Random();
            bool success = random.Next(1, 10) > 3;

            payment.Status = success ? "Success" : "Failed";

            if (success)
                payment.Order.Status = "Paid";

            await _context.SaveChangesAsync();

            return payment;
        }
    }
}
