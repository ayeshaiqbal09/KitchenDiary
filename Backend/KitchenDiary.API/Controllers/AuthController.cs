using KitchenDiary.API.DTOs.Auth;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KitchenDiary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    [HttpPost("register")]
public async Task<IActionResult> Register(RegisterRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var existingUser = await _userManager.FindByEmailAsync(request.Email);

    if (existingUser != null)
    {
        return Conflict("Email is already registered.");
    }

    var user = new ApplicationUser
    {
        FullName = request.FullName,
        UserName = request.Email,
        Email = request.Email
    };

    var result = await _userManager.CreateAsync(user, request.Password);

    if (!result.Succeeded)
    {
        return BadRequest(result.Errors);
    }

    return NoContent();
}
[HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var user = await _userManager.FindByEmailAsync(request.Email);

    if (user == null)
    {
        return Unauthorized("Invalid email or password.");
    }

    var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!passwordValid)
    {
        return Unauthorized("Invalid email or password.");
    }

    var token = await _tokenService.CreateTokenAsync(user);

    return Ok(new AuthResponse
    {
        Token = token,
        FullName = user.FullName,
        Email = user.Email!,
        ExpiresAt = DateTime.UtcNow.AddMinutes(60)
    });
}
}