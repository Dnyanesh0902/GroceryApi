using GroceryAPI.Models;

public interface IOrderRepository
{
    Task<Order?> PlaceOrder(string userId);
    Task<List<Order>> GetOrdersByUser(string userId);
    Task<List<Order>> GetAllOrders();
    Task<Order?> GetById(int id);
    Task<bool> CancelOrder(int orderId, string userId, bool isAdmin);
    Task<bool> UpdateStatus(int id, string status);
}