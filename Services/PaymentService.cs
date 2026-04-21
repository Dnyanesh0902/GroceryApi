using GroceryAPI.DTOs;
using GroceryAPI.Repository;

namespace GroceryAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;

        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaymentDto?> MakePayment(string userId, CreatePaymentDto dto)
        {
            var payment = await _repo.MakePayment(userId, dto);

            if (payment == null)
                return null;

            return new PaymentDto
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                Method = payment.Method
            };
        }
        public async Task<bool> RefundOrder(int orderId, string userId, bool isAdmin)
        {
            return await _repo.RefundOrder(orderId, userId, isAdmin);
        }
        public async Task<PaymentDto?> RetryPayment(string userId, int orderId)
        {
            var payment = await _repo.RetryPayment(userId, orderId);

            if (payment == null)
                return null;

            return new PaymentDto
            {
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Status = payment.Status,
                Method = payment.Method
            };
        }
    }
}
