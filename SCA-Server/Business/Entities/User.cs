using System.Text.Json.Serialization;
using Business.Enums;

namespace Business.Entities;

public class User : Common
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Elo { get; set; } = 1000;
    public Roles Role { get; set; } = Roles.User;

    public ICollection<EloHistory> EloHistories { get; set; } = new List<EloHistory>();
    public ICollection<TeamMembership> TeamMemberships { get; set; } = new List<TeamMembership>();
    [JsonIgnore]
    public ICollection<Match> MatchesAsPlayer1 { get; set; } = new List<Match>();
    [JsonIgnore]
    public ICollection<Match> MatchesAsPlayer2 { get; set; } = new List<Match>();
    [JsonIgnore]
    public ICollection<Match> MatchesAsOpponent1 { get; set; } = new List<Match>();
    [JsonIgnore]
    public ICollection<Match> MatchesAsOpponent2 { get; set; } = new List<Match>();
}