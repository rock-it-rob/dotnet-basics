using EntityFrameworkBasics.Notify.Data.Model;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkBasics.Notify.Data.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly ILogger<NotificationRepository> _log;
    private readonly NotificationContext _context;

    public NotificationRepository(NotificationContext context, ILogger<NotificationRepository> log)
        => (_context, _log) = (context, log);

    public void SaveChanges()
        => _context.SaveChanges();

    public string DetectChanges()
    {
        _context.ChangeTracker.DetectChanges();
        return _context.ChangeTracker.DebugView.LongView;
    }

    public void Create(Notification notification)
    {
        _context.Add(notification);
    }

    public Notification Read(long id)
    {
        return _context.Notifications!.Find(id)!;
    }
}