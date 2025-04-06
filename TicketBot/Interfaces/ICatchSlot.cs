using TicketBot.Dto;

namespace TicketBot.Interfaces;

public interface ICatchSlot
{
    Task<(List<ApiResponse> Office, string time)> GetCatchSlot(DateListDTO dateListDto);
}