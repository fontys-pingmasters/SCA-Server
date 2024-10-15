using Business.Dtos;
using Business.Entities;
using Business.Exceptions;
using Business.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IUserService userService, ITokenService tokenService) : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!userService.ValidateUser(request.Email, request.Password)) return Unauthorized();
        
        var user = userService.GetUserByEmail(request.Email);
        var token = tokenService.GenerateToken(user);
        
        return Ok(new { token });
    }
    
    [HttpPost]
    public IActionResult Register([FromBody] RegisterDto registerDto)
    {
        User user;
        
        try { user = userService.RegisterUser(registerDto); } 
        catch (Exception e) { return BadRequest(e.Message); }
        
        return Ok( new {user} );
    }
    
}