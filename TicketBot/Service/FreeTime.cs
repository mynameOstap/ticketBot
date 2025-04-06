using Newtonsoft.Json;
using TicketBot.Dto;
using TicketBot.Interfaces;

namespace TicketBot.Service;

public class FreeTime : IFreeTime
{
    private readonly IHttpClientWrapper _http;

    public FreeTime(IHttpClientWrapper http)
    {
        _http = http;
    }

    public async Task<TimeSlots?> GetFreeSlotAsync(int departId, string date)
    {
        string url =
            $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/slots?date={date}&page=1&pageSize=24";
        var response = await _http.GetAsync(url);
        var timeSlot = JsonConvert.DeserializeObject<TimeSlotResponseDTO>(response);
        return timeSlot?.Data?.LastOrDefault();
    }

    public async Task<string> CheckTimeAsync(string date, string time, int departId)
    {
        string url = $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/check";
        var postData = new SlotRequest()
        {
            Date = $"{date}T{time}",      
            ServiceId = 49,
            ServiceCenterId = departId   
        };
        var json = JsonConvert.SerializeObject(postData);
        return await _http.PostAsync(url, json);
    }

}