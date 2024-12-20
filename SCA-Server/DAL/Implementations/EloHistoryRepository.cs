using Business.Entities;
using Business.Exceptions;
using Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Implementations
{
	public class EloHistoryRepository(ApplicationDbContext context) : IEloHistoryRepository
	{
		public EloHistory CreateEloHistory(EloHistory eloHistory)
		{
			context.EloHistories.Add(eloHistory);
			context.SaveChanges();
			return context.EloHistories.FirstOrDefault(e => e.Id == eloHistory.Id) ?? throw new ResourceNotFoundException($"Elo history with id:{eloHistory.Id} not found");
		}
	}
}
