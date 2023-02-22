using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Create a Host for DI.
using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IFixedLengthReader, FixedLengthReader>();
    services.AddHostedService<App>();
}

host.Run();

class App : BackgroundService
{
    private readonly IHostApplicationLifetime applicationLifetime;
    private readonly IFixedLengthReader fixedLengthReader;
    private readonly ILogger<App> log;

    public App(IFixedLengthReader fixedLengthReader, ILogger<App> log, IHostApplicationLifetime applicationLifetime) =>
        (this.fixedLengthReader, this.log, this.applicationLifetime) = (fixedLengthReader, log, applicationLifetime);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        log.LogInformation("Testing fixed-length reader");

        // Test the fixed-length reader.
        await Task.Run(() => fixedLengthReader
            .readAndPrint(@"sample-files/plain-text.txt"));

        // Stop when finished.
        applicationLifetime.StopApplication();
    }

}