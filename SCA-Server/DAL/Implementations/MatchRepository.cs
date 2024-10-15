using Business.Entities;
using Business.Repositories;
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
	}
}
