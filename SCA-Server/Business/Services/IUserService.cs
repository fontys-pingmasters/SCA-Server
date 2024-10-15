using Business.Dtos;
using Business.Entities;

namespace Business.Services;

public interface IUserService
{
    User RegisterUser(RegisterDto registerDto);
    bool ValidateUser(string email, string password);
    User GetUserByEmail(string email);
}