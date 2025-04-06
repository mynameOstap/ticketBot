using Newtonsoft.Json;
using TicketBot.Dto;
using TicketBot.Interfaces;

namespace TicketBot.Service;

public class CatchSlot : ICatchSlot
{
    private readonly IHttpClientWrapper _http;

    public CatchSlot(IHttpClientWrapper http)
    {
        _http = http;
    }

    public async Task<(List<ApiResponse> Office, string time)> GetCatchSlot(DateListDTO dateListDto)
    {
        bool checkCatch = false;
        while (!checkCatch)
        {
            foreach (var day in dateListDto.Data)
            {
                string url = $"https://eqn.hsc.gov.ua/api/v2/departments?serviceId=49&date={day.Date:yyyy-MM-dd}";
                var response = await _http.GetAsync(url);
                var centers = JsonConvert.DeserializeObject<List<ApiResponse>>(response);

                var filtered = centers?.Where(c => c.srvCenterId == 129 || c.srvCenterId == 74).ToList();

                if (filtered != null && filtered.Any())
                {
                    checkCatch = true;
                    return (filtered, day.Date.ToString("yyyy-MM-dd"));
                }
                
                await Task.Delay(1000);
            }
            
        }

        return (new List<ApiResponse>(), string.Empty);
    }
}