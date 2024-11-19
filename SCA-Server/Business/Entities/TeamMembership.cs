namespace Business.Entities;

public class TeamMembership : Common
{
    public User User { get; set; }
    public Team Team { get; set; }
}