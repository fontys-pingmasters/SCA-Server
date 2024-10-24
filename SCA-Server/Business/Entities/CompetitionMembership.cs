namespace Business.Entities;

public class CompetitionMembership : Common
{
    public Team Team { get; set; }
    public Competition Competition { get; set; }
}