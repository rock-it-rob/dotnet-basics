using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkBasics.Data.Notification;

[PrimaryKey(nameof(Id))]
public class Notification
{
    public long? Id { get; set; }

    public required string Subject { get; set; }

    public required DateTime Updated { get; set; }
}