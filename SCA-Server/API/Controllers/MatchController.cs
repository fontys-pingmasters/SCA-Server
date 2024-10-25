using System.Security.Claims;
using Business.Dtos.RequestDtos;
using Business.Mappers;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    
    IMatchService _matchService;
    
    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult CreateMatch( [FromBody] CreateMatchRequest createMatchRequest)
    {
        createMatchRequest.CreatorId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var result = _matchService.CreateMatch(createMatchRequest);

        var match = MatchMapper.MatchToMatchDto(result);
        
        return Ok(match);
    }
}