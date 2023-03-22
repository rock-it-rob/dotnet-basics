using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkBasics.Data;

namespace EntityFrameworkBasics.Notification.Data;

public class NotificationRecipient : IUpdateTimestamp
{
    [Key]
    public long Id { get; set; }

    public required string EmailAddress { get; set; }

    [Required]
    public DateTime? Updated { get; set; }

    [Required]
    public long? NotificationId { get; set; }

    [Required]
    public Notification? notification { get; set; }

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }
}

public class NotificationRecipientConfiguration : IEntityTypeConfiguration<NotificationRecipient>
{
    public void Configure(EntityTypeBuilder<NotificationRecipient> entityBuilder)
    {
    }
}