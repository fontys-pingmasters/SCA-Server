using Business.Entities;
using Business.Exceptions;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;
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

		public List<EloHistory> GetAllEloHistoriesByUserId(int userId)
		{
			return context.EloHistories.Include(e => e.Match.Player1)
				.Include(e => e.Match.Player2)
				.Include(e => e.Match.Opponent1)
				.Include(e => e.Match.Opponent2)
				.Where(e => e.User.Id == userId)
				.OrderByDescending(e => e.Match.CreatedAt)
				.ToList();
		}
	}
}
