using System.Text.RegularExpressions;

namespace TicketBot.Service;

public class ParsingService
{
    public string ExtractCsrfToken(string html)
    {
        string csrfPattern = @"<meta\s+name=[""']csrf-token[""']\s+content=[""'](.*?)[""']";
        Match match = Regex.Match(html, csrfPattern);
        return match.Success ? match.Groups[1].Value : "Not Found";
    }

    public string ExtractValueParam(string html)
    {
        string valuePattern = @"data-params='.*?""value"":""(.*?)""";
        Match match = Regex.Match(html, valuePattern);
        return match.Success ? match.Groups[1].Value : "Not Found";
    }
}