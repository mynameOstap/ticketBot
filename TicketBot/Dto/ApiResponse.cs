using Newtonsoft.Json;

namespace TicketBot.Dto;

public class ApiResponse
{
    public int srvCenterId { get; set; }
    public string srvCenterName { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public int countryId { get; set; }
    public string countryName { get; set; }
    public int regionId { get; set; }
    public string regionName { get; set; }
    public int districtId { get; set; }
    public string districtName { get; set; }
    public int communityId { get; set; }
    public string communityName { get; set; }
    public int cityId { get; set; }
    public string cityName { get; set; }
    public string street { get; set; }
    public string building { get; set; }
    public string office { get; set; }
    public int allowCountPreReg { get; set; }
    public int allowCountTerminal { get; set; }
}


