namespace GroceryAPI.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; } = "Pending";
        // Pending, Success, Failed

        public DateTime PaidAt { get; set; } = DateTime.Now;

        public string Method { get; set; } // Card, UPI, COD
    }
}