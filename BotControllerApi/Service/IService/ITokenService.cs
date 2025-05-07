namespace BotControllerApi.Service.Interface;

public interface ITokenService
{
    public string GenerateToken(string id);
}