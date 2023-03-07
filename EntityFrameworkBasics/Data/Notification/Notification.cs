using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBasics.Data.Notification;

[PrimaryKey(nameof(Id))]
public class Notification
{
    public long? Id { get; set; }
    public string? Subject { get; set; }
    public DateTime? Updated { get; set; }
}