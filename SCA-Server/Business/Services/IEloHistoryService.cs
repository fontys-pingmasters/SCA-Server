﻿using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public interface IEloHistoryService
	{
		List<EloHistory> GetAllEloHistoriesByUserId(int userId);
	}
}
