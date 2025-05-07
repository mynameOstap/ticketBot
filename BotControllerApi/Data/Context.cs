using BotControllerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BotControllerApi.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
    public DbSet<UserModel> Users {get; set; }
}