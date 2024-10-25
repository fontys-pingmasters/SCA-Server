using System.Text.Json.Serialization;

namespace Business.Dtos.RequestDtos;

public class CreateMatchRequest
{
    [JsonIgnore]
    public int CreatorId { get; set; }
    public int? TeamMateId { get; set; }
    public int Opponent1Id { get; set; }
    public int? Opponent2Id { get; set; }
}