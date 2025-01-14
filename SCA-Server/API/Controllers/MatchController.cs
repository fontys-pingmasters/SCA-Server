using System.Security.Claims;
using Business.Dtos.RequestDtos;
using Business.Mappers;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SCA_Server.Hubs;

namespace SCA_Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MatchController : ControllerBase
{
    
    private readonly IMatchService _matchService;
    private readonly IHubContext<MatchHub> _matchHub;
    
    public MatchController(IMatchService matchService, IHubContext<MatchHub> matchHub)
    {
        _matchService = matchService;
        _matchHub = matchHub;
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateMatch( [FromBody] CreateMatchReq createMatchReq)
    {
        createMatchReq.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                                             throw new Exception("Could not find current user id in token"));
        
        var result = _matchService.CreateMatch(createMatchReq);

        var match = MatchMapper.MatchToMatchDto(result);
        
        return Ok(match);
    }
    
    [Authorize]
    [HttpPatch]
    public async Task<IActionResult> UpdateMatch([FromBody] UpdateMatchReq updateMatchReq)
    {
        updateMatchReq.CreatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                                             throw new Exception("Could not find current user id in token"));
        
        var result = _matchService.UpdateMatch(updateMatchReq);
        
        var match = MatchMapper.MatchToMatchDto(result);
        
        var allMatches = _matchService.GetAllMatches();


        await _matchHub.Clients.All.SendAsync("ReceiveMessage", allMatches);
        
        return Ok(match);
    }

    [HttpGet("{matchId}")]
    public IActionResult GetMatchById(int matchId)
    {
        var match = _matchService.GetMatchById(matchId);
        return Ok(match);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetMatchesByUserId()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ??
            throw new Exception("Could not find current user id in token"));

        var matches = _matchService.GetMatchesByUserId(userId);
        return Ok(matches);
    }

    [HttpGet]
    public IActionResult GetAllMatches()
    {
        var matches = _matchService.GetAllMatches();
        return Ok(matches);
    }
}