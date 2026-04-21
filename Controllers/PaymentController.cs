using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentController(IPaymentService service)
    {
        _service = service;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> MakePayment(CreatePaymentDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;

        var result = await _service.MakePayment(userId, dto);

        if (result == null)
            return BadRequest("Payment failed or invalid order");

        return Ok(result);
    }
    [Authorize]
    [HttpPut("refund/{id}")]
    public async Task<IActionResult> Refund(int id)
    {
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        bool isAdmin = role == "Admin";

        var result = await _service.RefundOrder(id, userId, isAdmin);

        if (!result)
            return BadRequest("Refund not allowed");

        return Ok("Refund successful");
    }
    [Authorize]
    [HttpPost("retry/{orderId}")]
    public async Task<IActionResult> RetryPayment(int orderId)
    {
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;

        var result = await _service.RetryPayment(userId, orderId);

        if (result == null)
            return BadRequest("Retry not allowed or payment already successful");

        return Ok(result);
    }
}