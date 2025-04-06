namespace TicketBot.Dto;

public class DateListDTO
{
    public List<DateItem> Data { get; set; }
}

public class DateItem
{
    public DateTime Date { get; set; }
}
