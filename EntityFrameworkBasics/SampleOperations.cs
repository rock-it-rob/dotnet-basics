using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EntityFrameworkBasics.Configuration;

namespace EntityFrameworkBasics;

public class SampleOperations
{
    public const string DB_OPTIONS_KEY = "Db";

    private readonly ILogger<SampleOperations> _log;
    private readonly IConfiguration _config;

    public SampleOperations(ILogger<SampleOperations> log, IConfiguration config) =>
        (_log, _config) = (log, config);

    public static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(AddServices)
            .Build();


        host
            .Services
            .GetService<SampleOperations>()!
            .Run();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<SampleOperations>();
    }

    private void Run()
    {
        _log.LogInformation("Starting");

        DbConfigurationOptions options = new();
        _config.Bind(DB_OPTIONS_KEY, options);

        options.Validate();

        _log.LogDebug($"{options}");
    }
}