using Business.Dtos.RequestDtos;
using Business.Entities;

namespace Business.Services;

public interface IMatchService
{
    Match CreateMatch(CreateMatchReq createMatchReq);
    Match UpdateMatch(UpdateMatchReq updateMatchReq);
    Match GetMatchById(int matchId);
    List<Match> GetAllMatches();
}