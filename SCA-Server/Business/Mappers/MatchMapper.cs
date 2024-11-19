using Business.Dtos.EntityDtos;
using Business.Entities;

namespace Business.Mappers;

public static class MatchMapper
{
    public static MatchDto MatchToMatchDto(Match match)
    {
        return new MatchDto
        {
            Id = match.Id,
            Player1 = UserMapper.UserToUserDto(match.Player1),
            Player2 = match.Player2 != null ? UserMapper.UserToUserDto(match.Player2) : null,
            Opponent1 = UserMapper.UserToUserDto(match.Opponent1),
            Opponent2 = match.Opponent2 != null ? UserMapper.UserToUserDto(match.Opponent2) : null,
            PlayerScore = match.PlayerScore,
            OpponentScore = match.OpponentScore,
            MatchRequests = match.MatchRequests.Select(MatchRequestMapper.MatchRequestToMatchRequestDto).ToList()
        };
    }
    
    
}