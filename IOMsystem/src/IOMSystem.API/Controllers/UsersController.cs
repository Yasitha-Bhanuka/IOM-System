using Microsoft.AspNetCore.Mvc;
using IOMSystem.Application.DTOs;
using IOMSystem.Application.Interfaces;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userService.LoginAsync(loginDto);
        if (user == null)
            return Unauthorized("Invalid email or password.");

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
    {
        var result = await _userService.RegisterUserAsync(registerDto);
        if (!result)
            return BadRequest("Registration failed. Email might already exist.");

        return Ok("User registered successfully.");
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
