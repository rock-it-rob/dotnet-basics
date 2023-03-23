using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntityFrameworkBasics.Data;

namespace EntityFrameworkBasics.Notify.Data.Model;

public class NotificationMessage : IUpdateTimestamp
{
    [Key]
    public long Id { get; set; }

    public required string Message { get; set; }

    [Required]
    public DateTime? Updated { get; set; }

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
    }
}