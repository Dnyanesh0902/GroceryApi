using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Repository;
using GroceryAPI.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }
    public async Task<OrderDto?> PlaceOrder(string userId)
    {
        var order = await _repo.PlaceOrder(userId);

        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt,
            Status = order.Status, // ✅ NOW WILL WORK
            Items = order.Items.Select(x => new OrderItemDto
            {
                ProductName = x.Product?.Name,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList()
        };
    }
    public async Task<IEnumerable<OrderDto>> GetOrdersByUser(string userId)
    {
        var orders = await _repo.GetOrdersByUser(userId);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            TotalAmount = o.TotalAmount,
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductName = i.Product.Name,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList()
        });
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        var orders = await _repo.GetAllOrders();

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            TotalAmount = o.TotalAmount,
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductName = i.Product.Name,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList()
        });
    }

    public async Task<bool> UpdateStatus(int id, string newStatus)
    {
        // ✅ Step 1: Validate allowed values
        var validStatuses = new[] { "Pending", "Shipped", "Delivered", "Cancelled" };

        if (!validStatuses.Contains(newStatus))
            return false;

        // ✅ Step 2: Get current order
        var order = await _repo.GetById(id);

        if (order == null)
            return false;

        var current = order.Status;

        // ✅ Step 3: Define allowed transitions
        var allowed = new Dictionary<string, List<string>>
    {
        { "Pending", new List<string> { "Shipped", "Cancelled" } },
        { "Shipped", new List<string> { "Delivered" } },
        { "Delivered", new List<string>() },
        { "Cancelled", new List<string>() }
    };

        // ✅ Step 4: Check transition
        if (!allowed[current].Contains(newStatus))
            return false;

        // ✅ Step 5: Update via repo
        return await _repo.UpdateStatus(id, newStatus);
    }
    public async Task<bool> CancelOrder(int orderId, string userId, bool isAdmin)
    {
        return await _repo.CancelOrder(orderId, userId, isAdmin);
    }
}