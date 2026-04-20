using GroceryAPI.Data;
using GroceryAPI.DTOs;
using GroceryAPI.Models;
using GroceryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;

    public AuthController(AppDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        string role = "Customer";

        if (dto.Role == "Maintenance" || dto.Role == "Admin")
        {
            role = dto.Role;
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        {
            UserName = dto.Username,
            PasswordHash = hashedPassword,
            Role = role   
        };
        if (_context.Users.Any(x => x.UserName == dto.Username))
        {
            return BadRequest("User already exists");
        }
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User Registered");
    }
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(x => x.UserName == dto.Username);

        if (user == null)
            return Unauthorized("Invalid Credentials");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid Credentials");

        var token = _authService.GenerateToken(user);

        return Ok(new { token });
    }
}