using GroceryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        // ✅ PLACE ORDER
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var order = await _service.PlaceOrder(userId);

            if (order == null)
                return BadRequest("Cart is empty");

            return Ok(order);
        }
        // 1. My Orders
        [Authorize(Roles = "User")]
        [HttpGet("my-orders")]
        public async Task<IActionResult> MyOrders()
        {
             var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _service.GetOrdersByUser(userId);

            return Ok(orders);
        }

        // 2. All Orders (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders);
        }

        // 3. Update Status
        [Authorize(Roles = "Admin")]
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var result = await _service.UpdateStatus(id, status);

            if (!result)
                return NotFound();

            return Ok("Status Updated");
        }
        // 4. Cancel Order
        [Authorize]
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var userId = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            bool isAdmin = role == "Admin";

            var result = await _service.CancelOrder(id, userId, isAdmin);

            if (!result)
                return BadRequest("Cannot cancel this order");

            return Ok("Order cancelled successfully");
        }
    }
}
