namespace GroceryAPI.Services.Gateway
{
    public class PaymentGatewayResponse
    {
        public bool IsSuccess { get; set; }

        public string TransactionId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
