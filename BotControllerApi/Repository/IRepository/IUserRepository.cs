using BotControllerApi.Models;

namespace BotControllerApi.Repository.IRepository;

public interface IUserRepository
{
    Task<UserModel?> GetByEmail(string email);
    Task<UserModel?> AddUserAsync(UserModel user);
}