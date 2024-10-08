namespace Common.Entities;

public class Club : Common
{
    public string ClubName { get; set; }
    public string Logo { get; set; }
    
    public ICollection<Team> Teams { get; set; }
}