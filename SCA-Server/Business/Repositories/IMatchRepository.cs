﻿using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
	public interface IMatchRepository
	{
		Task CreateMatch(Match match);
		Task<Match> GetMatchById(int matchId);
		Task UpdateMatch(Match match);
	}
}
