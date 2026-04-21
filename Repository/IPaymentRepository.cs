using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment?> MakePayment(string userId, CreatePaymentDto dto);
        Task<bool> RefundOrder(int orderId, string userId, bool isAdmin);
        Task<Payment?> RetryPayment(string userId, int orderId);
    }
}
