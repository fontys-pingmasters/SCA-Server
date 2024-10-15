using Business.Entities;
using Business.Repositories;

namespace DAL.Implementations;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{ 
    public User CreateUser(User user)
    {
        context.Users.Add(new User());
        context.SaveChanges();
        return context.Users.First();
    }

    public User GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}