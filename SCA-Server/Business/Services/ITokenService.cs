using Business.Entities;

namespace Business.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
}