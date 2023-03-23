using EntityFrameworkBasics.Notify.Data.Model;

namespace EntityFrameworkBasics.Notify.Service;

public interface INotificationService
{
    public Notification createNotification(ICollection<string> recipients, string subject, string message);
}