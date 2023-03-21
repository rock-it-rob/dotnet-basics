using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Data.Notification;

public class Notification
{
    [Key]
    public long Id { get; set; }

    public required string Subject { get; set; }

    public required DateTime Updated { get; set; }

    public List<NotificationMessage> NotificationMessages = new List<NotificationMessage>();

    // Can this setter be private?
    [Timestamp]
    public uint Version { get; private set; }
}