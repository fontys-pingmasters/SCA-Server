using Business.Entities;
using Business.Services;
using DAL.Dto_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCA_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MatchRequestController(IMatchRequestService matchRequestService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateMatchRequest(MatchRequestDto matchRequestDto)
		{
			MatchRequest newMatchRequest = new()
			{
				MatchId = matchRequestDto.MatchId,
				Status = matchRequestDto.Status
			};

			await matchRequestService.CreateMatchRequest(newMatchRequest);
			return Ok(newMatchRequest);
		}
	}
}
