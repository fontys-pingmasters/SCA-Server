using System.Text.Json.Serialization;
using Business.Enums;

namespace Business.Entities;

public class MatchRequest : Common
{
    public User Sender { get; set; }
    public User Receiver { get; set; }
    [JsonIgnore]
    public Match Match { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
}