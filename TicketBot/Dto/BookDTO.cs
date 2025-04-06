using Newtonsoft.Json;

namespace TicketBot.Dto;

public class BookDTO
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("serviceId")]
    public int ServiceId { get; set; }

    [JsonProperty("serviceCenterId")]
    public int ServiceCenterId { get; set; }
    [JsonProperty("config")]
    public Config Configs { get; set; }
}




public class Credential
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("phone")]
    public string Phone { get; set; }
    public Config Configs { get; set; }
}
public class Config
{
  
    [JsonProperty("tab")]
    public int Tab { get; set; }
    [JsonProperty("time")]
    public string Time { get; set; }
    [JsonProperty("department")] 
    public ApiResponse Department { get; set; }
    [JsonProperty("service")]
    public Service Services { get; set; }
    [JsonProperty("date")]
    public string? Date { get; set; }
    
    
}

public class Service
{
    [JsonProperty("groupId")]
    public int GroupId { get; set; }
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; }
}

