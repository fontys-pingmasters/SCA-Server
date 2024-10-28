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
    public IActionResult CreateMatch( [FromBody] CreateMatchReq createMatchReq)
    {
        createMatchReq.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                                             throw new Exception("Could not find current user id in token"));
        
        var result = _matchService.CreateMatch(createMatchReq);

        var match = MatchMapper.MatchToMatchDto(result);
        
        return Ok(match);
    }
    
    [Authorize]
    [HttpPatch]
    public IActionResult UpdateMatch([FromBody] UpdateMatchReq updateMatchReq)
    {
        updateMatchReq.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                                             throw new Exception("Could not find current user id in token"));
        
        var result = _matchService.UpdateMatch(updateMatchReq);
        
        var match = MatchMapper.MatchToMatchDto(result);
        
        return Ok(match);
    }
}