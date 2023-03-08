using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using EntityFrameworkBasics.Options;

namespace EntityFrameworkBasics.Data.Notification;

public class NotificationContext : AbstractDatabaseContext
{
    public DbSet<Notification>? Notifications { get; set; }
    public DbSet<NotificationMessage>? NotificationMessages { get; set; }

    public NotificationContext(
        DbContextOptions<NotificationContext> options,
        IOptions<DbConfigurationOptions> dbConfig,
        ILogger<NotificationContext> log
    ) : base(options, dbConfig, log)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        _log.LogInformation($"Configuring {nameof(NotificationContext)}");
    }
}