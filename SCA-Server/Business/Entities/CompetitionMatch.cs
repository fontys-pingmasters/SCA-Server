namespace Business.Entities;

public class CompetitionMatch : Common
{
    public Match Match { get; set; }
    public Competition Competition { get; set; }
}