using Serilog;
using Serilog.Events;

namespace AvaPMIS.Gateway;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting AvaPMIS Gateway");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<GatewayHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            //TODO used Yarp here
            //app.MapWhen((ctx) => !ctx.Request.Path.HasValue || ctx.Request.Path != @"/", (app) =>
            //{
            //    var webApp = app as WebApplication;
            //    webApp.MapReverseProxy();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.CreateApplicationBuilder().MapWhen((ctx) => !ctx.Request.Path.HasValue || ctx.Request.Path != @"/", (mapp) =>
                {
                    endpoints.MapReverseProxy();
                });
                
            });
            //app.MapReverseProxy();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "AvaPMIS.Gateway terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
