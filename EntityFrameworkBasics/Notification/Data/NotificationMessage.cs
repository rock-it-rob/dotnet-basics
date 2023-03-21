using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkBasics.Notification.Data;

public class NotificationMessage
{
    [Key]
    public long Id { get; set; }

    public required string Message { get; set; }

    [Required]
    public DateTime? Updated { get; private set; }

    [Required]
    public long? NotificationId { get; set; }

    [Required]
    public Notification? Notification { get; set; }

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }
}

public class NotificationMessageConfiguration : IEntityTypeConfiguration<NotificationMessage>
{
    public void Configure(EntityTypeBuilder<NotificationMessage> entityBuilder)
    {
        entityBuilder
            .Property<DateTime?>(n => n.Updated)
            .HasDefaultValueSql<DateTime?>("now()")
            .ValueGeneratedNever();
    }
}