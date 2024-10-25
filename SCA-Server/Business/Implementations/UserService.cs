using System.Text.RegularExpressions;
using Business.Dtos;
using Business.Dtos.RequestDtos;
using Business.Entities;
using Business.Exceptions;
using Business.Mappers;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public User GetUserById(int id)
    {
        return userRepository.GetUserById(id) ?? throw new ("User not found");
    }

    public User RegisterUser(RegisterRequest registerRequest)
    {
        if (userRepository.GetUserByEmail(registerRequest.Email) != null) throw new RegistrationException("Email already in use");
        if (registerRequest.Password != registerRequest.ConfirmPassword) throw new RegistrationException("Passwords do not match");
        if (Regex.IsMatch(registerRequest.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") == false) throw new RegistrationException("Invalid email format");
        if (Regex.IsMatch(registerRequest.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$") == false) throw new RegistrationException("Password must contain at least 8 characters, one uppercase letter, one lowercase letter and one number");
        
        User newUser = UserMapper.RegisterDtoToUser(registerRequest);
        
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        
        return userRepository.CreateUser(newUser);
    }

    public bool ValidateUser(string email, string password)
    {
        var user = userRepository.GetUserByEmail(email);
        
        if (user == null || !VerifyPassword(password, user.Password)) return false;
        
        return true;
    }

    public User GetUserByEmail(string email)
    {
        return userRepository.GetUserByEmail(email);
    }

    public List<User> GetAllUsers()
    {
        return userRepository.GetAllUsers();
    }

    public List<User> GetAllUsersExceptCurrentUser(int currentUserId)
    {
        return userRepository.GetAllUsers().Where(u => u.Id != currentUserId).ToList();
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}