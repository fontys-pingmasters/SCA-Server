using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.Entities;

public class User : Common
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
    
    public ICollection<EloHistory> EloHistories { get; set; }
    public ICollection<TeamMembership> TeamMemberships { get; set; }
    public ICollection<Match> MatchesAsPlayer1 { get; set; }
    public ICollection<Match> MatchesAsPlayer2 { get; set; }
    public ICollection<Match> MatchesAsOpponent1 { get; set; }
    public ICollection<Match> MatchesAsOpponent2 { get; set; }
}