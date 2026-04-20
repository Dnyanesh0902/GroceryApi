using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("all")]
        public IActionResult AllUser()
        {
            return Ok("All Logged-in Users Can Access This.");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Only Admin can access");
        }
        [Authorize(Roles = "Admin,Maintenance")]
        [HttpGet("Maintenance")]
        public IActionResult MaintenanceAndAdmin()
        {
            return Ok("Admin & Maintenance can access");
        }
    }
}
