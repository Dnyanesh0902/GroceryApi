using GroceryAPI.DTOs;

namespace GroceryAPI.Services
{
    public interface IPaymentService
    {
        Task<PaymentDto?> MakePayment(string userId, CreatePaymentDto dto);
        Task<bool> RefundOrder(int orderId, string userId, bool isAdmin);
        Task<PaymentDto?> RetryPayment(string userId, int orderId);
    }
}
