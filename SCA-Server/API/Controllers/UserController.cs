using System.Security.Claims;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    } 
    
    [Authorize]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var result = _userService.GetAllUsers();
        return Ok(result);
    }

    [Authorize]
    [HttpGet("Id")]
    public IActionResult GetUserById()
    {
		var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ??
			throw new Exception("Could not find current user id in token"));

        var user = _userService.GetUserById(userId);
        return Ok(user);
	}
    
    [Authorize]
    [HttpGet]
    [Route("exceptcurrent")]
    public IActionResult GetAllUsersExceptCurrentUser()
    {
        var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                                        throw new Exception("Could not find current user id in token"));
        
        var result = _userService.GetAllUsersExceptCurrentUser(currentUserId);
        return Ok(result);
    }
    
    
    
}