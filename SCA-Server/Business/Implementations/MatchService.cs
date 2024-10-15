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

		public async Task<Match?> GetMatchById(int matchId)
		{
			return await matchRepository.GetMatchById(matchId);
		}

		public async Task UpdateScore(int matchId, int scoringPlayerId)
		{
			var match = await matchRepository.GetMatchById(matchId) ?? throw new ArgumentException("Match not found.");
			if (match.IsCompleted)
			{
				throw new InvalidOperationException("Cannot update scores for a completed match.");
			}

			bool playerScore = scoringPlayerId == match.UserIdPlayer;
			bool opponentScore = scoringPlayerId == match.UserIdOpponent;
			int matchEndGameCount = 3;

			if (playerScore)
			{
				match.PlayerScore += 1;
			}
			else if (opponentScore)
			{
				match.OpponentScore += 1;
			}
			else
			{
				throw new ArgumentException("Invalid player ID.");
			}

			if (match.PlayerScore == matchEndGameCount || match.OpponentScore == matchEndGameCount)
			{
				match.IsCompleted = true;
			}

			await matchRepository.UpdateMatch(match);
		}
	}
}
