namespace Business.Entities;

public class Team : Common
{
    public int ClubId { get; set; }
    public Club Club { get; set; }
    public string TeamName { get; set; }

    public ICollection<TeamMembership> TeamMemberships { get; set; }
    public ICollection<CompetitionMembership> CompetitionMemberships { get; set; }
}