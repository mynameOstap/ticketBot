using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BotControllerApi;
using BotControllerApi.Data;
using BotControllerApi.Models;
using BotControllerApi.Repository;
using BotControllerApi.Repository.IRepository;
using BotControllerApi.Service;
using BotControllerApi.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
var configuration = configurationBuilder.Build();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.TypeInfoResolver = AppJsonContext.Default;
});
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
            .AllowAnyHeader()   
            .AllowAnyMethod(); 
    });
});
builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
        options.Events = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {

                context.Token = context.Request.Cookies["tasty-cookie"];
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowFrontend");
app.UseRouting();

app.Run();

