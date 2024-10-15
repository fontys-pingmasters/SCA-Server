using Business.Dtos;
using Business.Entities;
using Business.Enums;
using Business.Exceptions;
using Business.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!userService.ValidateUser(request.Email, request.Password)) return Unauthorized();
        
        var user = userService.GetUserByEmail(request.Email);
        var token = tokenService.GenerateToken(user);
        
        return Ok(new { token });
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto registerDto)
    {
        try { User user = userService.RegisterUser(registerDto); return Ok (new {user}); }
        catch (Exception e) { return BadRequest(e.Message); }
    }
    
}