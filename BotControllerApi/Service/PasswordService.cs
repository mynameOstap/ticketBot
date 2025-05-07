using BotControllerApi.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace BotControllerApi.Service;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public bool CheckPassword(string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }
}