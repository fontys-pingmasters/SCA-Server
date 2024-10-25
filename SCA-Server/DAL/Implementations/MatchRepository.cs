using Business.Entities;
using Business.Repositories;

namespace DAL.Implementations;

public class MatchRepository(ApplicationDbContext context) : IMatchRepository
{
    public Match CreateMatch(Match match)
    {
        context.Matches.Add(match);
        context.SaveChanges();
        return context.Matches.FirstOrDefault(m => m.Id == match.Id);
    }
}