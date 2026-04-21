namespace GroceryAPI.DTOs
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public string Method { get; set; } = string.Empty;
    }
}
