using System.Net;
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

    public async Task<ApiResponse> SendRequestAsync(Dictionary<string, string> postData)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/site/freetimes")
        {
            Content = new FormUrlEncodedContent(postData)
        };

        HttpResponseMessage response = await _client.SendAsync(request);
        var strReposonse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(strReposonse);
        return JsonConvert.DeserializeObject<ApiResponse>(strReposonse)!;

    }

    public async Task<ApiResponse> SenderAsync()
    {
        DateTime today = DateTime.Today;
        Random random = new Random();
        try
        {
            var officeIds = _configuration.GetSection("ApplicationRules:OfficeIds").Get<int[]>();
            if (officeIds == null || officeIds.Length == 0)
            {
                Console.WriteLine("Error: No office IDs found in configuration.");
                return null;
            }

            bool freeTime = false; 

            while (!freeTime) 
            {
                foreach (int officeId in officeIds) 
                { 
                    for (int j = 0; j < 10; j++)
                    {
                        DateTime targetDate = today.AddDays(j);

                        var postData = new Dictionary<string, string>
                        {
                            { "office_id", officeId.ToString() },
                            { "date_of_admission", targetDate.ToString("yyyy-MM-dd") },
                            { "question_id", "55" },
                            { "id_es", "" }
                        };

                        await Task.Delay(random.Next(1000,5000)); 
                        var result = await SendRequestAsync(postData);

                        if (result.Rows.Count > 0) 
                        {
                            freeTime = true; 
                            return result; 
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SenderAsync: {ex.Message}");
        }

        return null; 
    }

    public async Task<string> GetStep3(string timeId)
    {
        string gmail = _configuration.GetSection("Credentials:gmail").Get<string>();
        if (gmail == null)
        {
            return "Не введений gmail в appsettings";
        }
        var postData = new Dictionary<string, string>
        {
            { "question_id", "55" },
            { "id_chtime", timeId},
            { "email", gmail }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "https://eq.hsc.gov.ua/site/reservecherga")
        {
            Content = new FormUrlEncodedContent(postData)
        };

        HttpResponseMessage _response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

        if (_response.StatusCode == HttpStatusCode.Found) 
        {
            if (_response.Headers.Contains("X-Redirect"))
            {
                
                if (_response.Headers.TryGetValues("X-Redirect", out IEnumerable<string> redirectHeaders))
                {
                    string xRedirectUrl = redirectHeaders.FirstOrDefault();
                    HttpResponseMessage redirectResponse = await _client.GetAsync(xRedirectUrl);
                    string responseBody = await redirectResponse.Content.ReadAsStringAsync();
                    return responseBody; 
                }
            }
            else
            {
                Console.WriteLine("x-redirect header is missing.");
            }
          
        }

        return null;

    }

    public async Task<string> GetTicket(string csrf, string value)
    {
        var postData = new Dictionary<string, string>()
        {
            { "_csrf", csrf },
            { "value", value }
        };
        var request = new HttpRequestMessage(HttpMethod.Post, "/site/finish")
        {
            Content = new FormUrlEncodedContent(postData)
        };
        HttpResponseMessage responseMessage = await _client.SendAsync(request);
        return await responseMessage.Content.ReadAsStringAsync();
    }


}

