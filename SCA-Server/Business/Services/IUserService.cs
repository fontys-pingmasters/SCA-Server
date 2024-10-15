using Business.Entities;

namespace Business.Services;

public interface IUserService
{
    User CreateUser();
    bool ValidateUser(string email, string password);
    User GetUserByEmail(string email);
}