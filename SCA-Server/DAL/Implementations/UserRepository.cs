using Business.Entities;
using Business.Repositories;

namespace DAL.Implementations;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{ 
    public User CreateUser(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return context.Users.FirstOrDefault(u => u.Id == user.Id);
    }

    public User GetUserById(int id)
    {
        return context.Users.FirstOrDefault(u => u.Id == id);
    }
    public User? GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.Email == email);
    }

    public List<User> GetAllUsers()
    {
        return context.Users.ToList();
    }
}