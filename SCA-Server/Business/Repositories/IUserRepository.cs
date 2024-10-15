using Business.Entities;

namespace Business.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
    User CreateUser();
    Task<User?> GetUserById(int id);
    User GetUserByEmail(string email);
}