namespace GroceryAPI.DTOs
{
    public class GetProductDtos
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
