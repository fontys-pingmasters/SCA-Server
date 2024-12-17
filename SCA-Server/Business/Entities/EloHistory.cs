namespace Business.Entities;

public class EloHistory : Common
{
    public User User { get; set; }
    public int Elo { get; set; }
    public int EloChange { get; set; }
    public Match Match { get; set; }
}