using System.Text.Json.Serialization;
using BotControllerApi.Models;

namespace BotControllerApi;


[JsonSerializable(typeof(AuthRequestDto))]
[JsonSerializable(typeof(JwtOptions))]
[JsonSerializable(typeof(UserModel))]
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ApiResponse))]
public partial class AppJsonContext : JsonSerializerContext
{
}