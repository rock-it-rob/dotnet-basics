using Microsoft.EntityFrameworkCore;
namespace EntityFrameworkBasics.Data.Notification;

[PrimaryKey(nameof(Id))]
public class NotificationMessage
{
    public long? Id { get; set; }
    public required string Message { get; set; }
    public required DateTime Updated { get; set; }
}