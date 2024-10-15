using Business.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    
    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }
    
    [HttpPost]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (!_userService.ValidateUser(request.Email, request.Password))
        {
            return Unauthorized();
        }
        
        var user = _userService.GetUserByEmail(request.Email);
        var token = _tokenService.GenerateToken(user);
        
        return Ok(new { token });
    }
    
}