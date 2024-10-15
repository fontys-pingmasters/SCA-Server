using Business.Entities;
using Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
	public class MatchRequestRepository(ApplicationDbContext context) : IMatchRequestRepository
	{
		public async Task CreateMatchRequest(MatchRequest matchRequest)
		{
			await context.MatchRequests.AddAsync(matchRequest);
			await context.SaveChangesAsync();
		}
	}
}
