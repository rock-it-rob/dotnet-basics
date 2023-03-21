using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Notification.Data;

public class NotificationRecipient
{
    [Key]
    public long Id { get; set; }

    public required string EmailAddress { get; set; }

    // Set from the database by default.
    [Required]
    public DateTime? Updated { get; private set; }

    [Required]
    public long? NotificationId { get; set; }

    [Required]
    public Notification? notification { get; set; }

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }

    public NotificationRecipient()
    {
    }

    public NotificationRecipient(string emailAddress)
        => EmailAddress = emailAddress;
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