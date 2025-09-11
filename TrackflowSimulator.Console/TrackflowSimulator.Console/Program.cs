using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrackflowSimulator.Console.Application;
using TrackflowSimulator.Console.Core.Interfaces;
using TrackflowSimulator.Console.Infrastructure.Implementations;

class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // MassTransit setup with RabbitMQ
                services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    });
                });

                // Register your services
                services.AddScoped<IMessagePublisher, MassTransitPublisher>();
                services.AddScoped<ILocationGenerator, RandomLocationGenerator>();

                // Register the continuous simulator as a hosted service
                services.AddHostedService<LocationSimulatorService>();
            })
            .Build();

        Console.WriteLine("🚗 Simulator starting... Press Ctrl+C to stop.");

        await host.RunAsync(); // runs continuously until stopped
    }
}
