using Business.Enums;

namespace Business.Dtos.RequestDtos;

public class RegisterReq
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public Roles Role { get; set; } = Roles.User;
}