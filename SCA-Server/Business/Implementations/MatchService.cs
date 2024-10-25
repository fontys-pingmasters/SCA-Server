using Business.Dtos.RequestDtos;
using Business.Entities;
using Business.Enums;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class MatchService : IMatchService
{
    IUserRepository _userRepository;
    IMatchRepository _matchRepository;
    IMatchRequestRepository _matchRequestRepository;
    
    public MatchService(IUserRepository userRepository, IMatchRepository matchRepository, IMatchRequestRepository matchRequestRepository)
    {
        _userRepository = userRepository;
        _matchRepository = matchRepository;
        _matchRequestRepository = matchRequestRepository;
    }
    public Match CreateMatch(CreateMatchRequest createMatchRequest)
    {
        ValidateMatchRequest(createMatchRequest);
        
        Match match = new Match()
        {
            Player1 = _userRepository.GetUserById(createMatchRequest.CreatorId),
            Player2 = createMatchRequest.TeamMateId.HasValue ? _userRepository.GetUserById(createMatchRequest.TeamMateId.Value) : null,
            Opponent1 = _userRepository.GetUserById(createMatchRequest.Opponent1Id),
            Opponent2 = createMatchRequest.Opponent2Id.HasValue ? _userRepository.GetUserById(createMatchRequest.Opponent2Id.Value) : null
        };
        
        Match createdMatch = _matchRepository.CreateMatch(match);
        SendMatchRequests(createdMatch, createMatchRequest.CreatorId);
        
        return createdMatch;
    }
    private void SendMatchRequests(Match match, int senderUserId)
    {
        var matchParticipants = getMatchParticipants(match);
        
        foreach (var participant in matchParticipants)
        {
            var matchRequest = new MatchRequest()
            {
                Match = match,
                Sender = matchParticipants.First(u => u.Id == senderUserId),
                Receiver = participant,
                Status = senderUserId == participant.Id ? RequestStatus.Accepted : default
            };
            _matchRequestRepository.CreateMatchRequest(matchRequest);
        }
    }
    private void ValidateMatchRequest(CreateMatchRequest match)
    {
        List<User> matchParticipants = getMatchParticipants(match);
        
        if (match.Opponent2Id.HasValue != match.TeamMateId.HasValue)
            throw new Exception("Both teams must have either 1 or 2 player(s)");
        
        if(!matchParticipants.All(participant => matchParticipants.Count(u => u.Id == participant.Id) <= 1)) 
            throw new Exception("A user can only participate in a match once");
    }
    private List<User> getMatchParticipants(Match match)
    {
        List<User> matchParticipants =
        [
            match.Player1,
            match.Opponent1
        ];
        
        if (match is { Player2: not null, Opponent2: not null })
        {
            matchParticipants.Add(match.Player2);
            matchParticipants.Add(match.Opponent2);
        }
        
        return matchParticipants;
    }    
    private List<User> getMatchParticipants(CreateMatchRequest matchRequest)
    {
        List<User> matchParticipants =
        [
            _userRepository.GetUserById(matchRequest.CreatorId),
            _userRepository.GetUserById(matchRequest.Opponent1Id)
        ];
        
        if (matchRequest.TeamMateId.HasValue && matchRequest.Opponent2Id.HasValue)
        {
            matchParticipants.Add(_userRepository.GetUserById(matchRequest.TeamMateId.Value));
            matchParticipants.Add(_userRepository.GetUserById(matchRequest.Opponent2Id.Value));
        }
        
        return matchParticipants;
    }
}