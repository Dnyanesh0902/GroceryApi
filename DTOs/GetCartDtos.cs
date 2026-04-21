namespace GroceryAPI.DTOs
{
    public class GetCartDtos
    {
        public List<CartItemDto> Items { get; set; } = new();

        public decimal TotalAmount { get; set; }
    }
}
