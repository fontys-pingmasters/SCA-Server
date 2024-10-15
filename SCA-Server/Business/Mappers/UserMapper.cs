using Business.Dtos;
using Business.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static User RegisterDtoToUser(RegisterDto registerDto)
    {
        return new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Email = registerDto.Email,
            Password = registerDto.Password,
            Role = registerDto.Role
        };
    }
}