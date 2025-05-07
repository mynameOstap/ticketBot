using BotControllerApi.Data;
using BotControllerApi.Models;
using BotControllerApi.Repository.IRepository;
using BotControllerApi.Service;
using Microsoft.EntityFrameworkCore;

namespace BotControllerApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly Context _db;

    public UserRepository(Context db)
    {
        _db = db;

    }

    public async Task<UserModel> AddUserAsync(UserModel user)
    {

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(t => t.Email == email);
    }
}