using Newtonsoft.Json;

namespace TicketBot.Dto;

public class ApiResponse
{
    [JsonProperty("rows")] public List<Ticket> Rows { get; set; }

    public override string ToString()
    {
        return "Tickets:\n" + string.Join("\n", Rows);
    }
}

public class Ticket
{
    [JsonProperty("id")] 
    public string Id { get; set; }
    [JsonProperty("chtime")] public DateTime Time { get; set; }
    public override string ToString()
    {
        return $"ID: {Id}, Time: {Time}";
    }
}