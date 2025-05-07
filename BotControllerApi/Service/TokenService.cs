using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BotControllerApi.Models;
using BotControllerApi.Service.Interface;
using Microsoft.IdentityModel.Tokens;

namespace BotControllerApi.Service;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string id)
    {
        var jwtOptions = _configuration.GetSection("JwtOptions").Get<JwtOptions>();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, id)

        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtOptions.LifeTime)),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}