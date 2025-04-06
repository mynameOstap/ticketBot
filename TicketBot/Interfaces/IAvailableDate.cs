using TicketBot.Dto;

namespace TicketBot.Interfaces;

public interface IAvailableDate
{
    Task<DateListDTO> GetAvailableDatesAsync();
}