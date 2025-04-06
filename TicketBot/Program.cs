using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TicketBot.Dto;
using TicketBot.Service;


var builder = Host.CreateApplicationBuilder();
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.Configure<CredentialsConfig>(builder.Configuration.GetSection("Credentials"));
builder.Services.AddScoped<CreateRequestService>();
builder.Services.AddScoped<EmailSenderService>();
builder.Services.AddHttpClient<CreateRequestService>(client =>
{

    var credentials = configuration.GetSection("Credentials").Get<CredentialsConfig>();
    
  
    client.DefaultRequestHeaders.UserAgent.ParseAdd(credentials.UserAgent);
    client.DefaultRequestHeaders.Add("Cookie",credentials.Cookie);
    client.DefaultRequestHeaders.Add("Referer",credentials.Referer);

});

var app = builder.Build();
using (var scoped = app.Services.CreateScope())
{
    var requestservice = scoped.ServiceProvider.GetRequiredService<CreateRequestService>();
    var requestAsync = await requestservice.SendRequestAsync();
    var freeSlot = await requestservice.SenderAsync(requestAsync);
    var time = await requestservice.FreeTime(freeSlot.Office[0].srvCenterId,freeSlot.time);
    await requestservice.CheckTime(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId);
    await requestservice.BookTime(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId, freeSlot.Office[0]);
    await requestservice.CredentialBook(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId,freeSlot.Office[0]);
    await requestservice.Confirm(freeSlot.Office[0].srvCenterId);
    // var parsingService = scoped.ServiceProvider.GetRequiredService<ParsingService>();
    // var emailSender = scoped.ServiceProvider.GetRequiredService<EmailSenderService>();
    // var freeTimes = await requestservice.SenderAsync();
    // string htmlContent = await requestservice.GetStep3(freeTimes.Rows[0].Id);
    // string csrfToken = parsingService.ExtractCsrfToken(htmlContent);
    // string valueParam = parsingService.ExtractValueParam(htmlContent);
    // Console.WriteLine($"CSRF Token: {csrfToken}");
    // Console.WriteLine($"Value Param: {valueParam}");
    // string content = await requestservice.GetTicket(csrfToken, valueParam);
    // await emailSender.SenderEmailAsync(configuration.GetSection("Credentials:Gmail").Get<string>(),$"Талон взято на {freeTimes.Rows[0].Time.ToString()}");
}

