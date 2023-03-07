using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using EntityFrameworkBasics.Options;

namespace EntityFrameworkBasics.Data.Notification;

public class NotificationContext : DbContext
{
    public DbSet<Notification>? Notifications { get; set; }
    public DbSet<NotificationMessage>? NotificationMessages { get; set; }

    //private readonly ILogger<NotificationContext> _log;
    //public readonly DbConfigurationOptions _dbConfig;

    public NotificationContext(
        DbContextOptions<NotificationContext> options
        //IOptions<DbConfigurationOptions> dbConfig,
      //  ILogger<NotificationContext> log
    ) :
        base(options)
    {
      //  _dbConfig = dbConfig.Value;
        //_log = log;
        //_log.LogInformation($"Context constructed: {options}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        Console.WriteLine("Hello!");
        /*
        _log.LogInformation($"Configuring {nameof(NotificationContext)}");
        _log.LogDebug($"Using DB Options {_dbConfig}");

        options.UseNpgsql($"Host={_dbConfig.Host};Username={_dbConfig.User};Password={_dbConfig.Password}");
        */
        options.UseNpgsql("Host=localhost;Username=ef-user;Password=ef-password");
    }
}