using Business.Enums;

namespace Business.Dtos.EntityDtos;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Elo { get; set; }
    public string Email { get; set; } = string.Empty;
    public Roles Role { get; set; }
}