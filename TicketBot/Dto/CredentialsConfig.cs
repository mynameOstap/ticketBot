namespace TicketBot.Dto;


public class CredentialsConfig
    {
        public string Cookie { get; set; }
        public string UserAgent { get; set; }
        public string XCsrfToken { get; set; }
        public string XRequestedWith { get; set; }
        public string Referer { get; set; }
        public string Gmail { get; set; }
    }