using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntityFrameworkBasics.Data;

namespace EntityFrameworkBasics.Notify.Data.Model;

public class Notification : IUpdateTimestamp
{
    [Key]
    public long Id { get; set; }

    public required string Subject { get; set; }

    [Required]
    public DateTime? Updated { get; set; }

    public required NotificationMessage NotificationMessage { get; set; }

    public List<NotificationRecipient> NotificationRecipients { get; set; } = new List<NotificationRecipient>();

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }
}

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> entityBuilder)
    {
    }
}