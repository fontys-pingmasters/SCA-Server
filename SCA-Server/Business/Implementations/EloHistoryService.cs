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
	public class EloHistoryService : IEloHistoryService
	{
		IEloHistoryRepository _eloHistoryRepository;

		public EloHistoryService(IEloHistoryRepository eloHistoryRepository)
		{
			_eloHistoryRepository = eloHistoryRepository;
		}

		public List<EloHistory> GetAllEloHistoriesByUserId(int userId)
		{
			return _eloHistoryRepository.GetAllEloHistoriesByUserId(userId);
		}
	}
}
