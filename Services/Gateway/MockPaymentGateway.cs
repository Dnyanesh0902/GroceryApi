using System;

namespace GroceryAPI.Services.Gateway
{
    public class MockPaymentGateway : IPaymentGateway
    {
        public async Task<PaymentGatewayResponse> ProcessPayment(PaymentGatewayRequest request)
        {
            // simulate network delay (real payment gateway call)
            await Task.Delay(1000);

            // simulate success/failure (80% success rate)
            var random = new Random();
            bool isSuccess = random.Next(1, 10) > 2;

            if (isSuccess)
            {
                return new PaymentGatewayResponse
                {
                    IsSuccess = true,
                    TransactionId = Guid.NewGuid().ToString(),
                    Message = "Payment Successful"
                };
            }

            return new PaymentGatewayResponse
            {
                IsSuccess = false,
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Payment Failed"
            };
        }
    }
}