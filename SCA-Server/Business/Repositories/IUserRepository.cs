using Business.Entities;

namespace Business.Repositories;

public interface IUserRepository
{
    User CreateUser(User user);
    User? GetUserById(int id);
    User? GetUserByEmail(string email);
    List<User> GetAllUsers();
}