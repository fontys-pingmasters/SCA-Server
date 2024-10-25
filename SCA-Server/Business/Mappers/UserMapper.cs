using Business.Dtos;
using Business.Dtos.EntityDtos;
using Business.Dtos.RequestDtos;
using Business.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static User RegisterDtoToUser(RegisterRequest registerRequest)
    {
        return new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Role = registerRequest.Role
        };
    }
    
    public static UserDto UserToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }
}