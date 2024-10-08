namespace Common.Entities;

public class CompetitionMembership : Common
{
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public int CompetitionId { get; set; }
    public Competition Competition { get; set; }
}