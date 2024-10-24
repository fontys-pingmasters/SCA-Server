using Business.Entities;
using Business.Repositories;

namespace DAL.Implementations;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{ 
    public User CreateUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return context.Users.First();
    }

    public User GetUserById(int id)
    {
        throw new NotImplementedException();
    }
    public User? GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.Email == email);
    }
}