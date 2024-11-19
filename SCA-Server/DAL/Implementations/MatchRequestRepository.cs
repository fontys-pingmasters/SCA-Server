using Business.Entities;
using Business.Repositories;

namespace DAL.Implementations;

public class MatchRequestRepository(ApplicationDbContext context) : IMatchRequestRepository
{
    public MatchRequest CreateMatchRequest(MatchRequest matchRequest)
    {
        context.MatchRequests.Add(matchRequest);
        context.SaveChanges();
        return context.MatchRequests.First();
    }
}