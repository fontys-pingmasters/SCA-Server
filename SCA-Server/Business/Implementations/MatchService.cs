using Business.Dtos.RequestDtos;
using Business.Entities;
using Business.Enums;
using Business.Exceptions;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class MatchService : IMatchService
{
    IUserRepository _userRepository;
    IMatchRepository _matchRepository;
    IMatchRequestRepository _matchRequestRepository;
    IEloHistoryRepository _eloHistoryRepository;
    
    public MatchService(IUserRepository userRepository, IMatchRepository matchRepository,
        IMatchRequestRepository matchRequestRepository, IEloHistoryRepository eloHistoryRepository)
    {
        _userRepository = userRepository;
        _matchRepository = matchRepository;
        _matchRequestRepository = matchRequestRepository;
        _eloHistoryRepository = eloHistoryRepository;
    }
    public Match CreateMatch(CreateMatchReq createMatchReq)
    {
        ValidateMatchRequest(createMatchReq);
        
        Match match = new Match()
        {
            Player1 = _userRepository.GetUserById(createMatchReq.CreatorId) ?? 
                      throw new ResourceNotFoundException($"User with id:{createMatchReq.CreatorId} not found"),
            Player2 = createMatchReq.TeamMateId.HasValue ? _userRepository.GetUserById(createMatchReq.TeamMateId.Value) : null,
            Opponent1 = _userRepository.GetUserById(createMatchReq.Opponent1Id) ?? 
                        throw new ResourceNotFoundException($"User with id:{createMatchReq.Opponent1Id} not found"),
            Opponent2 = createMatchReq.Opponent2Id.HasValue ? _userRepository.GetUserById(createMatchReq.Opponent2Id.Value) : null
        };
        
        Match createdMatch = _matchRepository.CreateMatch(match);
        SendMatchRequests(createdMatch, createMatchReq.CreatorId);
        
        return createdMatch;
    }

    public Match UpdateMatch(UpdateMatchReq updateMatchReq)
    {
        var match = _matchRepository.GetMatchById(updateMatchReq.MatchId);

        if (match.Player1.Id != updateMatchReq.CreatorId)
            throw new Exception("Only the creator of the match can update it");

        match.PlayerScore = updateMatchReq.PlayerScore;
        match.OpponentScore = updateMatchReq.OpponentScore;

        var player1OldElo = match.Player1.Elo;
        var opponent1OldElo = match.Opponent1.Elo;

        var (player1NewElo, opponent1NewElo) = CalculateElo(match);

        match.Player1.Elo = player1NewElo;
        match.Opponent1.Elo = opponent1NewElo;

        _userRepository.UpdateUser(match.Player1);
        _userRepository.UpdateUser(match.Opponent1);

        EloHistory eloHistoryPlayer1 = new()
        {
            User = match.Player1,
            Elo = player1NewElo,
            EloChange = player1NewElo - player1OldElo,
            Match = match
        };

        EloHistory eloHistoryOpponent1 = new()
        {
            User = match.Opponent1,
            Elo = opponent1NewElo,
            EloChange = opponent1NewElo - opponent1OldElo,
            Match = match
        };

        _eloHistoryRepository.CreateEloHistory(eloHistoryPlayer1);
        _eloHistoryRepository.CreateEloHistory(eloHistoryOpponent1);
        
        return _matchRepository.UpdateMatch(match);
    }

	private static (int player1NewElo, int opponent1NewElo) CalculateElo(Match match)
	{
		const int K = 32; // Sensitivity constant

		double expectedPlayer1 = 1.0 / (1.0 + Math.Pow(10, (match.Opponent1.Elo - match.Player1.Elo) / 400.0));
		double expectedOpponent1 = 1.0 / (1.0 + Math.Pow(10, (match.Player1.Elo - match.Opponent1.Elo) / 400.0));

		// Determine actual results
		double actualPlayer1 = match.PlayerScore > match.OpponentScore ? 1 : (match.PlayerScore == match.OpponentScore ? 0.5 : 0);
		double actualOpponent1 = match.OpponentScore > match.PlayerScore ? 1 : (match.PlayerScore == match.OpponentScore ? 0.5 : 0);

		// New Elo ratings
		int newPlayer1Elo = (int)(match.Player1.Elo + K * (actualPlayer1 - expectedPlayer1));
		int newOpponent1Elo = (int)(match.Opponent1.Elo + K * (actualOpponent1 - expectedOpponent1));

		return (newPlayer1Elo, newOpponent1Elo);
	}


	public Match GetMatchById(int matchId)
    {
        return _matchRepository.GetMatchById(matchId);
    }

    public List<Match> GetMatchesByUserId(int userId)
    {
        return _matchRepository.GetMatchesByUserId(userId);
    }

    public List<Match> GetAllMatches()
    {
        return _matchRepository.GetAllMatches();
    }

    private void SendMatchRequests(Match match, int senderUserId)
    {
        var matchParticipants = GetMatchParticipants(match);
        
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
    private void ValidateMatchRequest(CreateMatchReq match)
    {
        var matchParticipants = GetMatchParticipants(match);
        
        if (match.Opponent2Id.HasValue != match.TeamMateId.HasValue)
            throw new Exception("Both teams must have either 1 or 2 player(s)");
        
        if(!matchParticipants.All(participant => matchParticipants.Count(u => u.Id == participant.Id) <= 1)) 
            throw new Exception("A user can only participate in a match once");
    }
    private static List<User> GetMatchParticipants(Match match)
    {
        List<User> matchParticipants =
        [
            match.Player1,
            match.Opponent1
        ];

        if (match is not { Player2: not null, Opponent2: not null }) return matchParticipants;
        matchParticipants.Add(match.Player2);
        matchParticipants.Add(match.Opponent2);

        return matchParticipants;
    }    
    private List<User> GetMatchParticipants(CreateMatchReq matchReq)
    {
        List<User> matchParticipants =
        [
            _userRepository.GetUserById(matchReq.CreatorId) ?? 
            throw new ResourceNotFoundException($"User with id:{matchReq.CreatorId} not found"),
            _userRepository.GetUserById(matchReq.Opponent1Id) ?? 
            throw new ResourceNotFoundException($"User with id:{matchReq.Opponent1Id} not found")
        ];

        if (!matchReq.TeamMateId.HasValue || !matchReq.Opponent2Id.HasValue) return matchParticipants;
        matchParticipants.Add(_userRepository.GetUserById(matchReq.TeamMateId.Value) ?? 
                              throw new ResourceNotFoundException($"User with id:{matchReq.TeamMateId.Value} not found"));
        matchParticipants.Add(_userRepository.GetUserById(matchReq.Opponent2Id.Value) ?? 
                              throw new ResourceNotFoundException($"User with id:{matchReq.Opponent2Id.Value} not found"));

        return matchParticipants;
    }
}