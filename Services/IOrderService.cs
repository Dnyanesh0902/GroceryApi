using GroceryAPI.DTOs;
using GroceryAPI.Models;

namespace GroceryAPI.Services
{
    public interface IOrderService
    {
        Task<OrderDto?> PlaceOrder(string userId);
        Task<IEnumerable<OrderDto>> GetOrdersByUser(string userId);
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<bool> UpdateStatus(int id, string status);
        Task<bool> CancelOrder(int orderId, string userId, bool isAdmin);
    }
}
