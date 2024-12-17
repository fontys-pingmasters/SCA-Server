using Business.Entities;

namespace Business.Repositories;

public interface IMatchRepository
{
    Match CreateMatch(Match match);
    Match UpdateMatch(Match match);
    Match GetMatchById(int matchId);
    List<Match> GetMatchesByUserId(int userId);
    List<Match> GetAllMatches();
    
}