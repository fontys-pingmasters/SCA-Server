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
	public class MatchRequestService(IMatchRequestRepository matchRequestRepository) : IMatchRequestService
	{
		public async Task CreateMatchRequest(MatchRequest matchRequest)
		{
			await matchRequestRepository.CreateMatchRequest(matchRequest);
		}
	}
}
