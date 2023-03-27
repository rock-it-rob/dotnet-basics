using EntityFrameworkBasics.Notify.Data.Model;

namespace EntityFrameworkBasics.Notify.Service;

public interface INotificationService
{
    public Notification CreateNotification(ICollection<string> recipients, string subject, string message);

    public void ChangeNotificationSubject(long id, string subject);
}