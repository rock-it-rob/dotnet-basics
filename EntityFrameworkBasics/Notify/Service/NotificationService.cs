namespace EntityFrameworkBasics.Notify.Service;

using EntityFrameworkBasics.Notify.Data.Model;
using EntityFrameworkBasics.Notify.Data.Repository;
using Microsoft.Extensions.Logging;

public class NotificationService
{
    private readonly INotificationRepository _repository;
    private readonly ILogger<NotificationService> _log;

    public NotificationService(INotificationRepository repository, ILogger<NotificationService> log)
        => (_repository, _log) = (repository, log);

    /// <summary>Creates a new Notification</summary>
    /// returns the newly created Notification.
    /// There must be at least one recipient provided. All recipients are email addresses.
    public Notification createNotification(ICollection<string> recipients, string subject, string message)
    {
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException(nameof(subject), "subject must be provided");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException(nameof(message), "message must be provided");

        if (recipients.Count == 0)
            throw new ArgumentException(nameof(recipients), "Must include at least one recipient");

        _log.LogInformation($"Creating Notification {subject}");

        var notificationMessage = new NotificationMessage { Message = "notification sample message" };
        var notification = new Notification { Subject = subject, NotificationMessage = notificationMessage };

        foreach (string recipient in recipients)
        {
            notification.NotificationRecipients.Add(new NotificationRecipient { EmailAddress = recipient });
        }

        _repository.Create(notification);

        _log.LogInformation(_repository.DetectChanges());

        _repository.SaveChanges();

        return notification;
    }
}