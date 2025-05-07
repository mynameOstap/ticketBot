namespace BotControllerApi.Models;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public string LifeTime { get; set; }
}