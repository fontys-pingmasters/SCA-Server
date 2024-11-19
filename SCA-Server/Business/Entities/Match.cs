namespace Business.Entities;

public class Match : Common
{
    public User Player1 { get; set; }
    public User? Player2 { get; set; }
    public User Opponent1 { get; set; }
    public User? Opponent2 { get; set; }
    public int PlayerScore { get; set; } = 0;
    public int OpponentScore { get; set; } = 0;
    
    public ICollection<MatchRequest> MatchRequests { get; set; } = new List<MatchRequest>();
}