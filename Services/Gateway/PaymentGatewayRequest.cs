namespace GroceryAPI.Services.Gateway
{
    public class PaymentGatewayRequest
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Method { get; set; } = string.Empty;
        // UPI, Card, NetBanking
    }
}
