using TicketBot.Dto;

namespace TicketBot.Interfaces;

public interface IBookingSlot
{
    Task<string> BookAsync(string date, string time, int departId, ApiResponse center);
    Task<string> SubmitUserAsync(string date, string time, int departId, ApiResponse center);
    Task<string> ConfirmAsync(int departId);
}