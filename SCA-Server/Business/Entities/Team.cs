namespace Business.Entities;

public class Team : Common
{
    public Club Club { get; set; }
    public string TeamName { get; set; }
    public ICollection<TeamMembership> TeamMemberships { get; set; } = new HashSet<TeamMembership>();
    public ICollection<CompetitionMembership> CompetitionMemberships { get; set; } = new HashSet<CompetitionMembership>();
}