using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TicketBot.Dto;
using TicketBot.Interfaces;
using TicketBot.Service;


var builder = Host.CreateApplicationBuilder();
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
builder.Services.AddSingleton<IConfiguration>(configuration);
builder.Services.Configure<CredentialsConfig>(builder.Configuration.GetSection("Credentials"));
builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>(client =>
{

    var credentials = configuration.GetSection("Credentials").Get<CredentialsConfig>();
    
  
    client.DefaultRequestHeaders.UserAgent.ParseAdd(credentials.UserAgent);
    client.DefaultRequestHeaders.Add("Cookie",credentials.Cookie);
    client.DefaultRequestHeaders.Add("Referer",credentials.Referer);


});
builder.Services.AddScoped<IAvailableDate, AvailableDate>();
builder.Services.AddScoped<IBookingSlot,BookingSlot>();
builder.Services.AddScoped<ICatchSlot,CatchSlot>();
builder.Services.AddScoped<IFreeTime,FreeTime>();
builder.Services.AddScoped<EmailSenderService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var availableDateService = scope.ServiceProvider.GetRequiredService<IAvailableDate>();
    var bookingSlotService = scope.ServiceProvider.GetRequiredService<IBookingSlot>();
    var catchSlotService = scope.ServiceProvider.GetRequiredService<ICatchSlot>();
    var freeTimeService = scope.ServiceProvider.GetRequiredService<IFreeTime>();
    var emailSenderService = scope.ServiceProvider.GetRequiredService<EmailSenderService>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();


    var availableDate = await availableDateService.GetAvailableDatesAsync();
    var catchSlot = await catchSlotService.GetCatchSlot(availableDate);
    var freeTime = await freeTimeService.GetFreeSlotAsync(catchSlot.Office[0].srvCenterId, catchSlot.time);
    await freeTimeService.CheckTimeAsync(catchSlot.time, freeTime.StartTime, catchSlot.Office[0].srvCenterId);
    await bookingSlotService.BookAsync(catchSlot.time, freeTime.StartTime, catchSlot.Office[0].srvCenterId,
        catchSlot.Office[0]);
    await bookingSlotService.SubmitUserAsync(catchSlot.time, freeTime.StartTime, catchSlot.Office[0].srvCenterId,
        catchSlot.Office[0]);
    await bookingSlotService.ConfirmAsync(catchSlot.Office[0].srvCenterId);
    // var requestservice = scoped.ServiceProvider.GetRequiredService<CreateRequestService>();
    // var requestAsync = await requestservice.SendRequestAsync();
    // var freeSlot = await requestservice.SenderAsync(requestAsync);
    // var time = await requestservice.FreeTime(freeSlot.Office[0].srvCenterId,freeSlot.time);
    // await requestservice.CheckTime(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId);
    // await requestservice.BookTime(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId, freeSlot.Office[0]);
    // await requestservice.CredentialBook(freeSlot.time, time.StartTime, freeSlot.Office[0].srvCenterId,freeSlot.Office[0]);
    // await requestservice.Confirm(freeSlot.Office[0].srvCenterId);

}

