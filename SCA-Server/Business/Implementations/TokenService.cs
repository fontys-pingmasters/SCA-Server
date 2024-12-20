using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Entities;
using Business.Services;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Implementations;

public class TokenService : ITokenService
{
    public TokenService()
    {
        Env.Load();
    }
    public string GenerateToken(User user)
    {
        var secretKey = Encoding.UTF8.GetBytes(Env.GetString("JWT_SECRET") ?? string.Empty);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, user.Role.ToString())
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(secretKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenOptions = new JwtSecurityToken(
            issuer: Env.GetString("JWT_ISSUER"),
            audience: Env.GetString("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(Env.GetString("JWT_EXPIRATION_TIME"))),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}