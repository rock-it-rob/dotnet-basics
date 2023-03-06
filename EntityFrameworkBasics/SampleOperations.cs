using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkBasics.Configuration;
using Microsoft.Extensions.Options;

namespace EntityFrameworkBasics;

public class SampleOperations : BackgroundService
{
    private readonly ILogger<SampleOperations> _log;
    private readonly DbConfigurationOptions _dbConfigurationOptions;
    private IHostApplicationLifetime _lifetime;

    public SampleOperations(ILogger<SampleOperations> log, IOptions<DbConfigurationOptions> dbConfigurationOptions, IHostApplicationLifetime lifetime) =>
        (_log, _dbConfigurationOptions, _lifetime) = (log, dbConfigurationOptions.Value, lifetime);

    public static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(AddServices)
            .Build();

        host.Run();
    }

    private static void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        // Services
        services.AddHostedService<SampleOperations>();

        // Options
        services.Configure<DbConfigurationOptions>(
            context.Configuration.GetSection(nameof(DbConfigurationOptions))
        );
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        await Task.Run(() =>
        {
            _log.LogInformation("Starting");

            _dbConfigurationOptions.Validate();

            _log.LogDebug($"{_dbConfigurationOptions}");
        });

        _lifetime.StopApplication();
    }
}