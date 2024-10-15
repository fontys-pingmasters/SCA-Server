using Business.Enums;

namespace Business.Entities;

public class MatchRequest : Common
{
    public int MatchId { get; set; }
    public Match Match { get; set; }
    public bool IsDoubleMatch { get; set; } = false;
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}