using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Notification.Data;

public class NotificationMessage
{
    [Key]
    public long Id { get; set; }

    public required string Message { get; set; }

    public required DateTime Updated { get; set; }

    public required long NotificationId { get; set; }

    public required Notification Notification { get; set; }

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }
}