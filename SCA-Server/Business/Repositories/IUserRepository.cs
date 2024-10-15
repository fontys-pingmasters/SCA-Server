using Business.Entities;

namespace Business.Repositories;

public interface IUserRepository
{
    User CreateUser();
    User GetUserById(int id);
    User GetUserByEmail(string email);
}