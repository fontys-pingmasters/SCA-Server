using Business.Dtos;
using Business.Dtos.RequestDtos;
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
    public IActionResult Login([FromBody] LoginReq request)
    {
        try {
            var token = userService.LoginUserReturnToken(request); 
            return Ok(new { token });
        }
        catch (ResourceNotFoundException e) { return BadRequest(e.Message); }
        catch (UnauthorizedException e) { return Unauthorized(e.Message); }
        catch (Exception e) { return StatusCode(500, e.Message); }
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterReq registerReq)
    {
        try
        {
            var user = userService.RegisterUser(registerReq); 
            return Ok (new {user});
        }
        catch (RegistrationException e) { return BadRequest(e.Message); }
        catch (Exception e) { return StatusCode(500, e.Message); }
    }
    
}