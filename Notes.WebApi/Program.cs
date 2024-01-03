using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Persistence;
using Serilog;
using Serilog.Events;
using System;

namespace Notes.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.File("NotesWebAppLog-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<NotesDbContext>();
                DbInitializer.Initialize(context);
            }
             catch (Exception e)
            {
                Log.Fatal(e, "An error occured!");
            }
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}