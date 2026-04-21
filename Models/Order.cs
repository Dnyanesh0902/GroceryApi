namespace GroceryAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public decimal TotalAmount { get; set; }
        public Payment Payment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public List<OrderItem> Items { get; set; } = new();
    }
}
