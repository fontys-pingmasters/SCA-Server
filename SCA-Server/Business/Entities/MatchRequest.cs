using Business.Enums;

namespace Business.Entities;

public class MatchRequest : Common
{
    public User Sender { get; set; }
    public User Receiver { get; set; } 
    public Match Match { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}