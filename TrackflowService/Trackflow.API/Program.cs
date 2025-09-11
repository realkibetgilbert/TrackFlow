using MassTransit;
using Microsoft.EntityFrameworkCore;
using Trackflow.API.Application;
using Trackflow.API.Application.Interfaces;
using Trackflow.API.Hubs;
using Trackflow.API.Infrastructure;
using Trackflow.API.Infrastructure.Messaging;
using Trackflow.API.Infrastructure.Persistance;
using Trackflow.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TrackflowConnection")
    )
);
builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices();
// Add services to the container.// SignalR
builder.Services.AddSignalR();

// Register the API-side notifier implementation
builder.Services.AddScoped<ILocationNotifier, SignalRLocationNotifier>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LocationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("location-created-queue", e =>
        {
            e.ConfigureConsumer<LocationConsumer>(context);
        });
    });
}); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularLocal", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("AllowAngularLocal");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<LocationHub>("/locationHub");

app.Run();
