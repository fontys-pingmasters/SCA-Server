namespace Business.Dtos.RequestDtos;

public class LoginReq
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}