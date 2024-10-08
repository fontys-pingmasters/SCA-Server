namespace Common.Entities;

public class Team
{
    public int ClubId { get; set; }
    public Club Club { get; set; }
    public string TeamName { get; set; }
    
    public ICollection<TeamMembership> TeamMemberships { get; set; }
    public ICollection<CompetitionMembership> CompetitionMemberships { get; set; }
}