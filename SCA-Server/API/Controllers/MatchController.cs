using Business.Entities;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA_Server.RequestModels;

namespace SCA_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MatchController(IMatchService matchService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateMatch(RequestModels.MatchRequest matchRequest)
		{
			Match newMatch = new()
			{
				UserIdPlayer = matchRequest.UserIdPlayer,
				UserIdPlayer2 = matchRequest.UserIdPlayer2,
				UserIdOpponent = matchRequest.UserIdOpponent,
				UserIdOpponent2 = matchRequest.UserIdOpponent2,
				IsDoubleMatch = matchRequest.IsDoubleMatch,
			};

			await matchService.CreateMatch(newMatch);
			return Ok(newMatch);
		}
	}
}
