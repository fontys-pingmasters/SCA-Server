using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SCA_Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EloHistoryController : ControllerBase
	{
		private readonly IEloHistoryService _eloHistoryService;

		public EloHistoryController(IEloHistoryService eloHistoryService)
		{
			_eloHistoryService = eloHistoryService;
		}

		[HttpGet]
		public IActionResult GetAllEloHistoriesByUserId()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ??
			throw new Exception("Could not find current user id in token"));

			var eloHistory = _eloHistoryService.GetAllEloHistoriesByUserId(userId);
			return Ok(eloHistory);
		}
	}
}
