using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkBasics.Data.Notification;

public class Notification
{
    [Key]
    public long Id { get; set; }

    public required string Subject { get; set; }

    public required DateTime Updated { get; set; }
}