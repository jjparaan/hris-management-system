using application.API.Contexts;
using application.API.Repositories.Implementations;
using application.API.Repositories.Interfaces;
using application.API.Services.Implementations.ApplicationLogs;
using application.API.Services.Interfaces.ApplicationLogs;
using application.API.Services.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

// Log service configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithProperty("RequestId", Guid.NewGuid())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Information)
        .WriteTo.File("Logs/application-information.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Error)
        .WriteTo.File("Logs/application-error.txt", rollingInterval: RollingInterval.Day)
    )
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Warning)
        .WriteTo.File("Logs/application-warning.txt", rollingInterval: RollingInterval.Day)
    )
    .CreateLogger();

var AllowedSpecificOrigins = "_allowedSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors(options => 
    options.AddPolicy(name: "AllowedSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    })
);

// Register dependency
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<LogQueue>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IJobTitleRepository, JobTitleRepository>();
builder.Services.AddHostedService<LogProcessingService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(AllowedSpecificOrigins);

// Middleware to catch unhandled exception
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogService>();
        await logger.LogErrorAsync("Unhandled exception occurred", "Middleware", ex);

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected server error occurred.");
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
