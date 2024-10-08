using Common.Enums;

namespace Common.Entities;

public class MatchRequest
{
    public int RequesterId { get; set; }
    public User Requester { get; set; }
    public int? TeammateId { get; set; }
    public User Teammate { get; set; }
    public int OpponentId { get; set; }
    public User Opponent1 { get; set; }
    public int? Opponent2Id { get; set; }
    public User Opponent2 { get; set; }
    public int MatchId { get; set; }
    public Match Match { get; set; }
    public bool IsDoubleMatch { get; set; } = false;
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}