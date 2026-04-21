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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var order = await _service.PlaceOrder(userId);

            if (order == null)
                return BadRequest("Cart is empty");

            return Ok(order);
        }
        // 1. My Orders
        [Authorize]
        [HttpGet("my-orders")]
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

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
    }
}
