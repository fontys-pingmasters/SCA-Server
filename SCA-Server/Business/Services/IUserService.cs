using Business.Entities;

namespace Business.Services;

public interface IUserService
{
    Task<List<User>> GetUsers();
    User CreateUser();
    bool ValidateUser(string email, string password);
    Task<User?> GetUserById(int id);
    User GetUserByEmail(string email);
}