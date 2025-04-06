using Newtonsoft.Json;
using TicketBot.Dto;
using TicketBot.Interfaces;

namespace TicketBot.Service;

public class AvailableDate : IAvailableDate
{
    private readonly IHttpClientWrapper _http;

    public AvailableDate(IHttpClientWrapper http)
    {
        _http = http;
    }

    public async Task<DateListDTO> GetAvailableDatesAsync()
    {
        string url = "https://eqn.hsc.gov.ua/api/v2/days?startDate=&endDate=&serviceId=49&departmentId=undefined";
        var response = await _http.GetAsync(url);
        return JsonConvert.DeserializeObject<DateListDTO>(response)!;
    }
}