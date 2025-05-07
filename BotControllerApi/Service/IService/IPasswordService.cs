namespace BotControllerApi.Service.Interface;

public interface IPasswordService
{
    public string HashPassword(string password);
    public bool CheckPassword(string password, string hashedPassword);
}