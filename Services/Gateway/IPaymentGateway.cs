using System.ComponentModel;

namespace GroceryAPI.Services.Gateway
{
    public interface IPaymentGateway
    {
        Task<PaymentGatewayResponse> ProcessPayment(PaymentGatewayRequest request);
    }

}
