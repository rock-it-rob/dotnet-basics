/// <summary>Holds commons functionality for all repository tests.</summary>
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using EntityFrameworkBasics.Options;

namespace EntityFrameworkBasics.Test;

public abstract class AbstractRepositoryTest
{
    protected static readonly IOptions<DbConfigurationOptions> DbConfiguration;

    protected DbDataSource? _dataSource;

    static AbstractRepositoryTest()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services
                    .AddOptions<DbConfigurationOptions>()
                    .Bind(context.Configuration.GetSection(nameof(DbConfigurationOptions)))
                    .ValidateDataAnnotations();
            })
            .Build();

        DbConfiguration = host.Services.GetRequiredService<IOptions<DbConfigurationOptions>>();
    }
}