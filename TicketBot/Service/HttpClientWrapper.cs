using System.Text;
using Newtonsoft.Json;
using TicketBot.Interfaces;

namespace TicketBot.Service;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _client;

    public HttpClientWrapper(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetAsync(string url)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        HttpResponseMessage response = await _client.SendAsync(request);
        var strContent = await response.Content.ReadAsStringAsync();
        return strContent;
    }

    public async Task<string> PostAsync<T>(string url, T postData)
    {
        var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content
        };
        HttpResponseMessage response = await _client.SendAsync(request);
        var strContent = await response.Content.ReadAsStringAsync();
        return strContent;
    }
    
    public async Task<string> PatchAsync<T>(string url, T postData)
    {
        var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = content
        };
        HttpResponseMessage response = await _client.SendAsync(request);
        var strContent = await response.Content.ReadAsStringAsync();
        return strContent;
    }
}