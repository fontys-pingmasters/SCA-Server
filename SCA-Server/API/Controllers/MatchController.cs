using Business.Entities;
using Business.Services;
using DAL.Dto_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MatchController(IMatchService matchService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateMatch(MatchDto matchDto)
		{
			Match newMatch = new()
			{
				UserIdPlayer = matchDto.UserIdPlayer,
				UserIdPlayer2 = matchDto.UserIdPlayer2,
				UserIdOpponent = matchDto.UserIdOpponent,
				UserIdOpponent2 = matchDto.UserIdOpponent2,
				IsDoubleMatch = matchDto.IsDoubleMatch,
			};

			await matchService.CreateMatch(newMatch);
			return Ok(newMatch);
		}
	}
}
