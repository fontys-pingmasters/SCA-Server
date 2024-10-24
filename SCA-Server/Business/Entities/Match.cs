namespace Business.Entities;

public class Match : Common
{
    public User Player1 { get; set; }
    public User? Player2 { get; set; }
    public User Opponent1 { get; set; }
    public User? Opponent2 { get; set; }
    public int PlayerScore { get; set; }
    public int OpponentScore { get; set; }
    public bool IsDoubleMatch { get; set; } = false;

    public ICollection<Match> MatchesAsPlayer1 { get; set; } = new HashSet<Match>();
    public ICollection<Match> MatchesAsPlayer2 { get; set; } = new HashSet<Match>();
    public ICollection<Match> MatchesAsOpponent1 { get; set; } = new HashSet<Match>();
    public ICollection<Match> MatchesAsOpponent2 { get; set; } = new HashSet<Match>();
}