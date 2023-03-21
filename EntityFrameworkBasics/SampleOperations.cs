using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkBasics.Options;
using EntityFrameworkBasics.Notification.Data;
using Microsoft.Extensions.Options;

namespace EntityFrameworkBasics;

public class SampleOperations
{
    private readonly ILogger<SampleOperations> _log;
    private readonly DbConfigurationOptions _dbConfigurationOptions;
    private readonly NotificationContext _notificationContext;

    public SampleOperations(
        ILogger<SampleOperations> log,
        IOptions<DbConfigurationOptions> dbConfigurationOptions,
        NotificationContext notificationContext
    )
    {
        _log = log;
        _dbConfigurationOptions = dbConfigurationOptions.Value;
        _notificationContext = notificationContext;
    }

    public static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(AddServices)
            .Build();

        var ops = host.Services.GetRequiredService<SampleOperations>();

        host.StartAsync();
        ops.Execute();
        host.StopAsync();
    }

    private static void AddServices(HostBuilderContext context, IServiceCollection services)
    {
        // Services
        services.AddScoped<SampleOperations>();

        // Options
        services.AddOptions<DbConfigurationOptions>()
            .Bind(context.Configuration.GetSection(nameof(DbConfigurationOptions)))
            .ValidateDataAnnotations();

        //
        // Configure the Db Context in its OnConfigure method. Options are needed from DI.
        //

        services.AddDbContext<NotificationContext>();
    }

    protected void Execute()
    {
        _log.LogInformation("Starting");

        var nots = _notificationContext.Notifications!.Count();

        _log.LogInformation($"Total Notifications = {nots}");

        //_lifetime.StopApplication();
    }
}