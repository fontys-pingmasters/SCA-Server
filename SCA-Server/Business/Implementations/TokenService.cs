using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Entities;
using Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}