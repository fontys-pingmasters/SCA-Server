using Business.Entities;

namespace Business.Repositories;

public interface IMatchRepository
{
    Match CreateMatch(Match match);
    
}