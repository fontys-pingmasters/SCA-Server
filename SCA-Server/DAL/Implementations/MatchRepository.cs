using Business.Entities;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
	public class MatchRepository(ApplicationDbContext context) : IMatchRepository
	{
		public async Task CreateMatch(Match match)
		{
			await context.Matches.AddAsync(match);
			await context.SaveChangesAsync();
		}

		public async Task<Match?> GetMatchById(int matchId)
		{
			return await context.Matches
				.Include(m => m.Player1)
				.Include(m => m.Player2)
				.Include(m => m.Opponent1)
				.Include(m => m.Opponent2)
				.FirstOrDefaultAsync(m => m.Id == matchId);
		}

		public async Task UpdateMatch(Match match)
		{
			context.Matches.Update(match);
			await context.SaveChangesAsync();
		}
	}
}
