using TicketBot.Dto;

namespace TicketBot.Interfaces;

public interface IFreeTime
{
    Task<TimeSlots?> GetFreeSlotAsync(int departId, string date);
    Task<string> CheckTimeAsync(string date, string time, int departId);

}