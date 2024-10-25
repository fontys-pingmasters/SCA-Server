using Business.Dtos;
using Business.Dtos.RequestDtos;
using Business.Entities;

namespace Business.Services;

public interface IUserService
{
    User GetUserById(int id);
    User RegisterUser(RegisterRequest registerRequest);
    bool ValidateUser(string email, string password);
    User GetUserByEmail(string email);
    List<User> GetAllUsers();
    List<User> GetAllUsersExceptCurrentUser(int currentUserId);
}