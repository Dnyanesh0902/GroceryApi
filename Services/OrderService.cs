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

    public async Task<bool> UpdateStatus(int id, string status)
    {
        return await _repo.UpdateStatus(id, status);
    }
}