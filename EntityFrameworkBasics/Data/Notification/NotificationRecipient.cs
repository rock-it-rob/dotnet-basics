using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Data.Notification;

public class NotificationRecipient
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string? EmailAddress { get; set; }

    // Set from the database by default.
    [Required]
    public DateTime? Updated { get; private set; }

    [Required]
    public long? NotificationId { get; set; }

    [Required]
    public Notification? notification { get; set; }
}

public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
{
    public void Configure(EntityTypeBuilder<NotificationRecipient> entityBuilder)
    {
        entityBuilder
            .Property<DateTime?>(n => n.Updated)
            .HasDefaultValueSql<DateTime?>("now()")
            .ValueGeneratedNever();
    }
}