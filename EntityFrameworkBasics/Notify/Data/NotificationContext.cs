using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using EntityFrameworkBasics.Options;
using EntityFrameworkBasics.Data;
using EntityFrameworkBasics.Notify.Data.Model;

namespace EntityFrameworkBasics.Notify.Data;

public class NotificationContext : AbstractDatabaseContext
{
    public DbSet<Notification>? Notifications
        => Set<Notification>();

    public DbSet<NotificationMessage>? NotificationMessages
        => Set<NotificationMessage>();

    public DbSet<NotificationRecipient>? NotificationRecipients
        => Set<NotificationRecipient>();

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

        if (!new List<EntityState> { EntityState.Added, EntityState.Modified }.Contains(eventArgs.Entry.State))
            return;

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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationContext).Assembly);
    }
}