using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Data.Notification;

public class NotificationMessage
{
    [Key]
    public long Id { get; set; }

    public required string Message { get; set; }

    public required DateTime Updated { get; set; }

    public required long NotificationId { get; set; }

    public required Notification Notification { get; set; }
}