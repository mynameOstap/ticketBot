using TicketBot.Dto;
using TicketBot.Interfaces;

namespace TicketBot.Service;

public class BookingSlot : IBookingSlot
{
    private readonly IHttpClientWrapper _http;

    public BookingSlot(IHttpClientWrapper http)
    {
        _http = http;  
    } 

    public async Task<string> BookAsync(string date, string time, int departId, ApiResponse center)
    {
        var data = new BookDTO
        {
            Date = $"{date}T{time}",
            ServiceId = 49,
            ServiceCenterId = departId,
            Configs = new Config
            {
                Tab = 4,
                Time = time,
                Department = center,
                Services = new Dto.Service
                {
                    GroupId = 7,
                    Id = 49,
                    Title = "категорія В (механічна КПП)"
                }
            }
        };

        string url = $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book";
        return await _http.PostAsync(url, data);
    }

    public async Task<string> SubmitUserAsync(string date, string time, int departId, ApiResponse center)
    {
        var data = new Credential
        {
            Name = "",
            Email = "",
            Phone = "",
            Configs = new Config
            {
                Date = $"{date}T{time}",
                Tab = 5,
                Time = time,
                Department = center,
                Services = new Dto.Service
                {
                    GroupId = 7,
                    Id = 49,
                    Title = "категорія В (механічна КПП)"
                }
            }
        };

        string url = $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book";
        return await _http.PatchAsync(url, data);
    }

    public async Task<string> ConfirmAsync(int departId)
    {
        string url = $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book/confirm";
        return await _http.PostAsync(url, new { });
    }
}
