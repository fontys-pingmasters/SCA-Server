using System.Text.RegularExpressions;
using Business.Dtos;
using Business.Dtos.RequestDtos;
using Business.Entities;
using Business.Exceptions;
using Business.Mappers;
using Business.Repositories;
using Business.Services;

namespace Business.Implementations;

public partial class UserService() : IUserService
{
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    private static partial Regex EmailRegex();
    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
    private static partial Regex PasswordRegex();
    
    private readonly IUserRepository userRepository;
    private readonly ITokenService tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService) : this()
    {
        this.userRepository = userRepository;
        this.tokenService = tokenService;
    }
    
    public User GetUserById(int id)
    {
        return userRepository.GetUserById(id) ?? throw new ("User not found");
    }

    public User RegisterUser(RegisterReq registerReq)
    {
        if (userRepository.GetUserByEmail(registerReq.Email) != null) 
            throw new RegistrationException("Email already in use");
        if (registerReq.Password != registerReq.ConfirmPassword) 
            throw new RegistrationException("Passwords do not match");
        if (EmailRegex().IsMatch(registerReq.Email) == false) 
            throw new RegistrationException("Invalid email format");
        if (PasswordRegex().IsMatch(registerReq.Password) == false) 
            throw new RegistrationException("Password must contain at least 8 characters, one uppercase letter, one lowercase letter and one number");
        
        var newUser = UserMapper.RegisterDtoToUser(registerReq);
        
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        
        return userRepository.CreateUser(newUser);
    }

    public string LoginUserReturnToken(LoginReq loginReq)
    {
        var user = userRepository.GetUserByEmail(loginReq.Email) ?? 
                   throw new ResourceNotFoundException($"User with email:{loginReq.Email} not found");
        
        if (!VerifyPassword(loginReq.Password, user.Password)) 
            throw new UnauthorizedException("Invalid password");
        
        return tokenService.GenerateToken(user);
    }
    
    public User GetUserByEmail(string email)
    {
        return userRepository.GetUserByEmail(email) ?? 
               throw new ResourceNotFoundException($"User with email:{email} not found");
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