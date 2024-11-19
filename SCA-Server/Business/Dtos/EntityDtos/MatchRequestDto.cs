using Business.Enums;

namespace Business.Dtos.EntityDtos;

public class MatchRequestDto
{
    public int Id { get; set; }
    public UserDto Sender { get; set; }
    public UserDto Receiver { get; set; }
    public RequestStatus Status { get; set; }
    public int MatchId { get; set; }
}