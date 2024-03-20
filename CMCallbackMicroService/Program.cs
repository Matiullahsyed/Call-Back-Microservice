using AccountManagementMicroService.Middleware;
using CallbackMicroService.Data.Repository;
using CallbackMicroService.Domain.Interface;
using CallbackMicroService.Logger;
using CMCallbackMicroService.Data.Context;
using CMCallbackMicroService.Data.Repository;
using CMCallbackMicroService.Domain.Interface;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Staging"}.json", true)
        .Build();

//var configue = new ConfigurationBuilder()
//        .SetBasePath(Directory.GetCurrentDirectory())
//        .AddJsonFile("appsettings.json")
//        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Staging"}.json", true)
//        .Build();
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(configuration)
.Enrich.FromLogContext()
.CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
//builder.Services.AddSingleton<BaseRepository>();

//builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddTransient<ICBService, CBService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddTransient<ICPSProcess, CPSProcess>();
builder.Services.AddScoped<IBaseRepository, BaseRepository>();

builder.Services.AddHttpClient("CMM", httpClient =>
{
    httpClient.BaseAddress = new Uri(configuration.GetValue<string>("responseCodeURL"));
});
builder.Services.AddHttpClient("SMM", httpClient =>
{
    httpClient.BaseAddress = new Uri(configuration["Session:Configuration"]);
});
builder.Services.AddDbContext<CMCallbackEFContext>(
    o => {
        o.UseNpgsql(builder.Configuration.GetConnectionString("CallbackDB"));
        o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });
//builder.Services.AddDbContext<ProfileContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddMassTransit(x =>
//{
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
//    {

//        config.Host(new Uri(configuration.GetValue<string>("RabbitMQ:URL")), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//    }));
//});
//ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction() || app.Environment.prep)
//{
//app.UseFormatMSISDN();
app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x.WithOrigins(configuration.GetSection("Origins").GetChildren().Select(x => x.Value).ToArray()));
app.Run();
