using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TicketBot.Dto;

namespace TicketBot.Service;

public class CreateRequestService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public CreateRequestService(HttpClient client, IConfiguration configuration)
    {
        _configuration = configuration;
        _client = client;
    }

    public async Task<DateListDTO> SendRequestAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://eqn.hsc.gov.ua/api/v2/days?startDate=&endDate=&serviceId=49&departmentId=undefined");
       

        HttpResponseMessage response = await _client.SendAsync(request);
        var strReposonse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(strReposonse);
        var result =  JsonConvert.DeserializeObject<DateListDTO>(strReposonse)!;
        return result;

    }

    public async Task<(List<ApiResponse> Office, string time)> SenderAsync(DateListDTO dateListDto)
    {
      
        try
        {
                for (int i = 0; i < dateListDto.Data.Count; i++) 
                { 
                    var request = new HttpRequestMessage(HttpMethod.Get, $"https://eqn.hsc.gov.ua/api/v2/departments?serviceId=49&date=" +
                                                                         $"{dateListDto.Data[i].Date.ToString("yyyy-MM-dd")}");
                    HttpResponseMessage response = await _client.SendAsync(request);
                    var strReposonse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(strReposonse);
                    var centers = JsonConvert.DeserializeObject<List<ApiResponse>>(strReposonse);
                    var filteredCenters = centers.Where(center => center.srvCenterId == 129 || center.srvCenterId == 74).ToList();
                    if (filteredCenters.Any())
                    {
                        
                        return (filteredCenters,  dateListDto.Data[i].Date.ToString("yyyy-MM-dd"));
                    }
                    await Task.Delay(1000);
                }
            
        }
    
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SenderAsync: {ex.Message}");
        }
    
        return (new List<ApiResponse>(), string.Empty);
    }

    public async Task<TimeSlots?> FreeTime(int departId, string date)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/slots?date={date}&page=1&pageSize=24");
        
            HttpResponseMessage response = await _client.SendAsync(request);
            var strResponse = await response.Content.ReadAsStringAsync();
        
            var timeSlotResponse = JsonConvert.DeserializeObject<TimeSlotResponseDTO>(strResponse);

            if (timeSlotResponse?.Data != null && timeSlotResponse.Data.Any())
            {
                Console.WriteLine(timeSlotResponse.Data.Last());
                return timeSlotResponse.Data.Last(); 
            }

            return null; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in FreeSlots: {ex.Message}");
            return null;
        }
    }

    public async Task<string> CheckTime(string date, string time, int departId)
    {
        var postData = new SlotRequest()
        {
            Date = $"{date}T{time}",      
            ServiceId = 49,
            ServiceCenterId = departId   
        };

        var json = JsonConvert.SerializeObject(postData);

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/check")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage responseMessage = await _client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }
    public async Task<string> BookTime(string date, string time, int departId, ApiResponse apiResponse)
    {
        var postData = new BookDTO()
        {
            Date = $"{date}T{time}",
            ServiceId = 49,
            ServiceCenterId = departId,
            Configs = new Config()
            {
                 
                    Tab = 4,
                    Time = time,
                    Department = apiResponse,
                    Services = new Dto.Service()
                    {

                        GroupId = 7,
                        Id = 49,
                        Title = "категорія В (механічна КПП)"
                        
                    }

            }
            
        };

        var json = JsonConvert.SerializeObject(postData);

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage responseMessage = await _client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }

    public async Task<string> CredentialBook(string date, string time,int departId,ApiResponse apiResponse)
    {
        var postData = new Credential()
        {
            Name = "",
            Email = "",
            Phone = "",
            Configs = new Config()
            {
                

                    Date = $"{date}T{time}",
                    Tab = 5,
                    Time = time,
                    Department = apiResponse,
                    Services = new Dto.Service()
                    {
                        
                        GroupId = 7,
                        Id = 49,
                        Title = "категорія В (механічна КПП)"
                        
                    }
                }

            
        };
        
        

        var json = JsonConvert.SerializeObject(postData);

        var request = new HttpRequestMessage(HttpMethod.Patch, $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage responseMessage = await _client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }
    public async Task<string> Confirm(int departId)
    {
        
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"https://eqn.hsc.gov.ua/api/v2/departments/{departId}/services/49/book/confirm");
        
        HttpResponseMessage responseMessage = await _client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }
    
}

