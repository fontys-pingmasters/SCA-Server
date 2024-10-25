using Business.Dtos;
using Business.Entities;
using Business.Enums;
using Business.Exceptions;
using Business.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Business.Dtos.RequestDtos.RegisterRequest;

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
    public IActionResult Register([FromBody] RegisterRequest registerRequest)
    {
        try { User user = userService.RegisterUser(registerRequest); return Ok (new {user}); }
        catch (Exception e) { return BadRequest(e.Message); }
    }
    
}