using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace TicketBot.Service;

public class EmailSenderService
{
    private readonly IConfiguration _configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SenderEmailAsync(string recipientEmail, string message)
    {
        var botGmail = _configuration.GetSection("GmailSenderSetting:BotGmail").Get<string>();
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Ticket", botGmail));
        emailMessage.To.Add(new MailboxAddress("", recipientEmail));  
        emailMessage.Subject = "Ваш Талон в МВС";
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var smtp = new SmtpClient())
        {
            try
            {
                await smtp.ConnectAsync(_configuration.GetSection("GmailSenderSetting:Host").Get<string>(), 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(botGmail, _configuration.GetSection("GmailSenderSetting:Password").Get<string>());
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка при відправці: {e.Message}");
                throw;
            }
        }
    }
}
