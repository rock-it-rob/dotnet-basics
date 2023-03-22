using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using EntityFrameworkBasics.Options;
using EntityFrameworkBasics.Data;

namespace EntityFrameworkBasics.Notification.Data;

public class NotificationContext : AbstractDatabaseContext
{
    public DbSet<Notification>? Notifications { get; set; }
    public DbSet<NotificationMessage>? NotificationMessages { get; set; }
    public DbSet<NotificationRecipient>? NotificationRecipients { get; set; }

    public NotificationContext(
        DbContextOptions<NotificationContext> options,
        IOptions<DbConfigurationOptions> dbConfig,
        ILogger<NotificationContext> log
    ) : base(options, dbConfig, log)
    {
        // Subscribe to the detect changes event in order to update the timestamps
        // on each entity type that supports it.
        ChangeTracker.DetectedEntityChanges += OnDetectChanges;
    }

    private void OnDetectChanges(object? sender, DetectedEntityChangesEventArgs eventArgs)
    {
        _log.LogDebug($"OnDetectChanges for {eventArgs.Entry}");

        if (eventArgs.Entry.Entity is IUpdateTimestamp u)
        {
            _log.LogDebug($"Updating timestamp on {u}");
            u.Updated = DateTime.UtcNow;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
        _log.LogInformation($"Configuring {nameof(NotificationContext)}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the entity method on each entity to fire their configuring interfaces.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationRecipientConfiguration).Assembly);
    }
}