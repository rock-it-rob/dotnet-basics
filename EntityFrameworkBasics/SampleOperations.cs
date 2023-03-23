using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkBasics.Options;
using Microsoft.Extensions.Options;
using EntityFrameworkBasics.Notify.Service;
using EntityFrameworkBasics.Notify.Data;

namespace EntityFrameworkBasics;

public class SampleOperations
{
    private readonly ILogger<SampleOperations> _log;
    private readonly DbConfigurationOptions _dbConfigurationOptions;
    private readonly NotificationService _notificationService;

    public SampleOperations(
        ILogger<SampleOperations> log,
        IOptions<DbConfigurationOptions> dbConfigurationOptions,
        NotificationService notificationService
    )
    {
        _log = log;
        _dbConfigurationOptions = dbConfigurationOptions.Value;
        _notificationService = notificationService;
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
        services.AddScoped<NotificationService>();


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

        var notification = _notificationService.createNotification(new List<string> { "sample@email.com" }, "Sample subject", "Sample message");

        _log.LogInformation($"Create Notification {notification.Id}");
    }
}