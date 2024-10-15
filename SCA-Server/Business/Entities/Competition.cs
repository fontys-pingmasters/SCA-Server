namespace Business.Entities;

public class Competition : Common
{
    public string CompetitionName { get; set; }

    public ICollection<CompetitionMembership> CompetitionMemberships { get; set; }
    public ICollection<CompetitionMatch> CompetitionMatches { get; set; }
}