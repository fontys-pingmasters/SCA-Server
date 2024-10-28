using System.Text.Json.Serialization;

namespace Business.Dtos.RequestDtos;

public class UpdateMatchReq
{
    public int MatchId { get; set; }
    [JsonIgnore]
    public int CreatorId { get; set; }
    public int PlayerScore { get; set; }
    public int OpponentScore { get; set; }
}