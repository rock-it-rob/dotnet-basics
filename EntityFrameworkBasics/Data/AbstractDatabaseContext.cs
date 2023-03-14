using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EntityFrameworkBasics.Options;

namespace EntityFrameworkBasics.Data;

public abstract class AbstractDatabaseContext : DbContext
{
    protected readonly ILogger _log;
    protected readonly DbConfigurationOptions _dbConfig;

    public AbstractDatabaseContext(
        DbContextOptions options,
        IOptions<DbConfigurationOptions> dbConfig,
        ILogger log
    ) : base(options)
        => (_dbConfig, _log) = (dbConfig.Value, log);

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        _log.LogDebug($"Using DB Options {_dbConfig}");

        options
            .UseNpgsql($"Host={_dbConfig.Host};Username={_dbConfig.User};Password={_dbConfig.Password}")
            .UseSnakeCaseNamingConvention();
    }
}