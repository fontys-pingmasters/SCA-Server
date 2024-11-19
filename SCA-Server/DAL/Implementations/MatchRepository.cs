using Business.Entities;
using Business.Exceptions;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class MatchRepository(ApplicationDbContext context) : IMatchRepository
{
    public Match CreateMatch(Match match)
    {
        context.Matches.Add(match);
        context.SaveChanges();
        return context.Matches.FirstOrDefault(m => m.Id == match.Id) ?? throw new ResourceNotFoundException($"Match with id:{match.Id} not found");
    }

    public Match UpdateMatch(Match match)
    {
        context.Matches.Update(match);
        context.SaveChanges();
        return context.Matches.FirstOrDefault(m => m.Id == match.Id) ?? throw new ResourceNotFoundException($"Match with id:{match.Id} not found");
    }

    public Match GetMatchById(int matchId)
    {
        return context.Matches.Include(m => m.Player1)
            .Include(m => m.Player2)
            .Include(m => m.Opponent1)
            .Include(m => m.Opponent2)
            .FirstOrDefault(m => m.Id == matchId) ?? throw new ResourceNotFoundException($"Match with id:{matchId} not found");
    }
}