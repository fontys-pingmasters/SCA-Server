using Business.Entities;
using Business.Repositories;
using Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
	public class MatchService(IMatchRepository matchRepository) : IMatchService
	{
		public async Task CreateMatch(Match match)
		{
			await matchRepository.CreateMatch(match);
		}
	}
}
