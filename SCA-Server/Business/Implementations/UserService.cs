using Business.Entities;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<List<User>> GetUsers()
    {
        return await userRepository.GetUsers();
    }

    public User CreateUser()
    {
        throw new NotImplementedException();
    }

    public bool ValidateUser(string email, string password)
    {
        var user = userRepository.GetUserByEmail(email);
        
        if (user == null || !VerifyPassword(password, user.Password))
        {
            return false;
        }
        
        return true;
    }

    public async Task<User?> GetUserById(int id)
    {
        return await userRepository.GetUserById(id);
    }

    public User GetUserByEmail(string email)
    {
        return userRepository.GetUserByEmail(email);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}