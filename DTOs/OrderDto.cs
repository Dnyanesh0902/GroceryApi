namespace GroceryAPI.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> Items { get; set; }

    }
}
