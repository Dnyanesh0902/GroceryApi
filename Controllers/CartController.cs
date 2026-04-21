using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GroceryAPI.Services;
using GroceryAPI.DTOs;

namespace GroceryAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
       private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddToCartDto dto)
        {
            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            await _cartService.AddToCart(userid, dto.ProductId, dto.Quantity);
            return Ok("Added To Cart");
        }

    }
}
