using Microsoft.EntityFrameworkCore;
namespace EntityFrameworkBasics.Data.Notification;

[PrimaryKey(nameof(Id))]
public class NotificationMessage
{
    public long? Id { get; set; }
    public string? Message { get; set; }
    public DateTime? Updated { get; set; }
}