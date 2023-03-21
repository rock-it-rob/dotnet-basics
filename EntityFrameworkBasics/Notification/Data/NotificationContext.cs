using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        // Add a trigger to populate the updated column on default updates.
        modelBuilder
            .Entity<Notification>(
                builder =>
                    builder.Metadata.AddTrigger(
                        @"create trigger n1_trigger before insert or update on notifications
	                        for each row execute procedure set_update()"
                    )
            );

    }
}