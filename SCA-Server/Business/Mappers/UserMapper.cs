using Business.Dtos;
using Business.Dtos.EntityDtos;
using Business.Dtos.RequestDtos;
using Business.Entities;

namespace Business.Mappers;

public static class UserMapper
{
    public static User RegisterDtoToUser(RegisterReq registerReq)
    {
        return new User
        {
            FirstName = registerReq.FirstName,
            LastName = registerReq.LastName,
            Email = registerReq.Email,
            Password = registerReq.Password,
            Role = registerReq.Role
        };
    }
    
    public static UserDto UserToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Elo = user.Elo,
            Email = user.Email,
            Role = user.Role
        };
    }
}