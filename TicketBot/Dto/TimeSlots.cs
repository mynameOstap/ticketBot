using Newtonsoft.Json;

namespace TicketBot.Dto;

public class TimeSlots
{
    [JsonProperty("startTime")]
    public string StartTime { get; set; }

    [JsonProperty("stopTime")]
    public string StopTime { get; set; }

    [JsonProperty("isAllowed")]
    public bool IsAllowed { get; set; }

    [JsonProperty("jobCount")]
    public int JobCount { get; set; }

    [JsonProperty("allowedJobCount")]
    public int AllowedJobCount { get; set; }
}

public class TimeSlotResponseDTO
{
    [JsonProperty("data")]
    public List<TimeSlots> Data { get; set; }

}


public class SlotRequest
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("serviceId")]
    public int ServiceId { get; set; }

    [JsonProperty("serviceCenterId")]
    public int ServiceCenterId { get; set; }
}