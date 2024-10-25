namespace Business.Dtos.EntityDtos;

public class MatchDto
{
    public int Id { get; set; }
    public UserDto Player1 { get; set; }
    public UserDto? Player2 { get; set; }
    public UserDto Opponent1 { get; set; }
    public UserDto? Opponent2 { get; set; }
    public int PlayerScore { get; set; } = 0;
    public int OpponentScore { get; set; } = 0;
    
    public ICollection<MatchRequestDto> MatchRequests { get; set; } = new List<MatchRequestDto>();
}