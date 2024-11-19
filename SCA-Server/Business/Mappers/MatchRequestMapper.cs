using Business.Dtos.EntityDtos;
using Business.Entities;

namespace Business.Mappers;

public static class MatchRequestMapper
{
    public static MatchRequestDto MatchRequestToMatchRequestDto(MatchRequest matchRequest)
    {
        return new MatchRequestDto
        {
            Id = matchRequest.Id,
            Sender = UserMapper.UserToUserDto(matchRequest.Sender),
            Receiver = UserMapper.UserToUserDto(matchRequest.Receiver),
            Status = matchRequest.Status,
            MatchId = matchRequest.Match.Id
        };
    }
    
}