using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkBasics.Notification.Data;

public class Notification
{
    [Key]
    public long Id { get; set; }

    public required string Subject { get; set; }

    [Required]
    public DateTime? Updated { get; private set; }

    public required NotificationMessage NotificationMessage;

    public List<NotificationRecipient> NotificationRecipients = new List<NotificationRecipient>();

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }

    public Notification()
    {
    }

    public Notification(string subject, NotificationMessage notificationMessage)
        => (Subject, NotificationMessage) = (subject, notificationMessage);
}

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> entityBuilder)
    {
        entityBuilder
            .Property<DateTime?>(n => n.Updated)
            .HasDefaultValueSql<DateTime?>("now()")
            .ValueGeneratedNever();
    }
}