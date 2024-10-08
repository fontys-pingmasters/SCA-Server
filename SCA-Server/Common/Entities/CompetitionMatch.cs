namespace Common.Entities;

public class CompetitionMatch : Common
{
    public int? MatchId { get; set; }
    public Match Match { get; set; }
    public int CompetitionId { get; set; }
    public Competition Competition { get; set; }
}