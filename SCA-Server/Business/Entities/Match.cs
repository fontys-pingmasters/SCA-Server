namespace Business.Entities;

public class Match : Common
{
    public int UserIdPlayer { get; set; }
    public User Player1 { get; set; }
    public int? UserIdPlayer2 { get; set; }
    public User? Player2 { get; set; }
    public int UserIdOpponent { get; set; }
    public User Opponent1 { get; set; }
    public int? UserIdOpponent2 { get; set; }
    public User? Opponent2 { get; set; }
    public int PlayerScore { get; set; } = 0;
    public int OpponentScore { get; set; } = 0;
    public bool IsDoubleMatch { get; set; } = false;
    public bool IsCompleted { get; set; } = false;

    public ICollection<CompetitionMatch>? CompetitionMatches { get; set; }
}