using Business.Entities;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public User CreateUser()
    {
        throw new NotImplementedException();
    }

    public bool ValidateUser(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);
        
        if (user == null || !VerifyPassword(password, user.Password))
        {
            return false;
        }
        
        return true;
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}