namespace Business.Entities;

public class Competition : Common
{
    public string CompetitionName { get; set; }

    public ICollection<CompetitionMembership> CompetitionMemberships { get; set; } = new List<CompetitionMembership>();
    public ICollection<CompetitionMatch> CompetitionMatches { get; set; } = new List<CompetitionMatch>();
}