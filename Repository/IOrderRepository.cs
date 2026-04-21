using GroceryAPI.Models;

public interface IOrderRepository
{
    Task<Order?> PlaceOrder(string userId);
    Task<List<Order>> GetOrdersByUser(string userId);
    Task<List<Order>> GetAllOrders();
    Task<bool> UpdateStatus(int id, string status);
}