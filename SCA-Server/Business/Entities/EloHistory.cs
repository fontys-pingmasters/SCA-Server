namespace Business.Entities;

public class EloHistory : Common
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int Elo { get; set; }
    public int? MatchId { get; set; }
    public Match Match { get; set; }
}