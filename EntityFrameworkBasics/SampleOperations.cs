using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkBasics.Options;
using EntityFrameworkBasics.Data.Notification;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBasics;

public class SampleOperations : BackgroundService
{
    private readonly ILogger<SampleOperations> _log;
    private readonly DbConfigurationOptions _dbConfigurationOptions;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly NotificationContext _notificationContext;

    public SampleOperations(
        ILogger<SampleOperations> log,
        IOptions<DbConfigurationOptions> dbConfigurationOptions,
        IHostApplicationLifetime lifetime,
        NotificationContext notificationContext
    )
    {
        _log = log;
        _dbConfigurationOptions = dbConfigurationOptions.Value;
        _lifetime = lifetime;
        _notificationContext = notificationContext;
    }

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
        services.AddOptions<DbConfigurationOptions>()
            .Bind(context.Configuration.GetSection(nameof(DbConfigurationOptions)))
            .ValidateDataAnnotations();

        //
        // Configure the Db Context in its OnConfigure method. Options are needed from DI.
        //

        services.AddDbContext<NotificationContext>();
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        await Task.Run(() =>
        {
            _log.LogInformation("Starting");
            _log.LogInformation($"{_notificationContext}");
            //_log.LogDebug($"{_dbConfigurationOptions}");
        });

        _lifetime.StopApplication();
    }
}