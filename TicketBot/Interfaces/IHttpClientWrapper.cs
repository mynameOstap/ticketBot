namespace TicketBot.Interfaces;

public interface IHttpClientWrapper 
{
    Task<string> GetAsync(string url);
    Task<string> PostAsync<T>(string url, T data);
    Task<string> PatchAsync<T>(string url, T data);
}