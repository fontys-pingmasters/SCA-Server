using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult Post()
    {
        return Ok();
    }
    
    [HttpPut]
    public IActionResult Put()
    {
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok();
    }
    
}