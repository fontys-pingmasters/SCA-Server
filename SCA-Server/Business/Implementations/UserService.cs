using System.Text.RegularExpressions;
using Business.Dtos;
using Business.Entities;
using Business.Exceptions;
using Business.Mappers;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public class UserService(IUserRepository userRepository) : IUserService
{
    public User RegisterUser(RegisterDto registerDto)
    {
        if (userRepository.GetUserByEmail(registerDto.Email) != null) throw new RegistrationException("Email already in use");
        if (registerDto.Password != registerDto.ConfirmPassword) throw new RegistrationException("Passwords do not match");
        if (Regex.IsMatch(registerDto.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") == false) throw new RegistrationException("Invalid email format");
        if (Regex.IsMatch(registerDto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$") == false) throw new RegistrationException("Password must contain at least 8 characters, one uppercase letter, one lowercase letter and one number");
        
        User newUser = UserMapper.RegisterDtoToUser(registerDto);
        
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

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}