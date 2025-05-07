using BotControllerApi.Models;
using BotControllerApi.Repository.IRepository;
using BotControllerApi.Service;
using BotControllerApi.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BotControllerApi.Controller;

[ApiController]
[Route("api/")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _token;

    public UserController(IUserRepository userRepository,IPasswordService passwordService,ITokenService token)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _token = token;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRequestDto dto)
    {
        if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            return BadRequest(new ApiResponse(){ success = false, message = "Email та пароль обов'язкові" });

        var existUser = await _userRepository.GetByEmail(dto.Email);
        if (existUser != null)
            return Conflict(new ApiResponse(){ success = false, message = "Такий користувач вже існує" });

        var newUser = new UserModel
        {
            Email = dto.Email,
            hashedPassword = _passwordService.HashPassword(dto.Password)
        };

        await _userRepository.AddUserAsync(newUser);
        return Ok(new ApiResponse(){ success = true,message = "Користувач створений"});
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto dto)
    {
        var existUser = await _userRepository.GetByEmail(dto.Email);
        if (existUser == null || !_passwordService.CheckPassword(dto.Password, existUser.hashedPassword))
        {
            return Unauthorized(new ApiResponse(){ success = false, message = "Неправильний пароль або користувач не існує" });
        }

        var token = _token.GenerateToken(existUser.Id.ToString());
        Response.Cookies.Append("tasty-cookie", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new ApiResponse(){ success = true, message =  token });
    }
    
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("tasty-cookie"); 
        return Ok(new ApiResponse(){ success = true, message = "Ви вийшли з системи" });
    }

    [HttpGet("checkauth")]
    public IActionResult CheckAuth()
    {
        var token = Request.Cookies["tasty-cookie"];
    
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized(new ApiResponse(){ success = false, message = "Неавторизований" });
        }
        return Ok(new ApiResponse(){ success = true, message = token});
    }

}