using Business.Entities;
using Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<List<User>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    public User CreateUser()
    {
        context.Users.Add(new User());
        context.SaveChanges();
        return context.Users.First();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}