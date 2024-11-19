using Business.Entities;

namespace Business.Repositories;

public interface IMatchRequestRepository
{
    MatchRequest CreateMatchRequest(MatchRequest matchRequest);
}