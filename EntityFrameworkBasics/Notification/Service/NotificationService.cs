namespace EntityFrameworkBasics.Notification.Service;

using EntityFrameworkBasics.Notification.Data;
using Microsoft.Extensions.Logging;

public class NotificationService
{
    private readonly NotificationContext _notificationContext;
    private readonly ILogger<NotificationService> _log;

    public NotificationService(NotificationContext notificationContext, ILogger<NotificationService> log)
        => (_notificationContext, _log) = (notificationContext, log);

    /// <summary>Creates a new Notification</summary>
    /// returns the newly created Notification.
    /// There must be at least one recipient provided. All recipients are email addresses.
    public Notification createNotification(ICollection<string> recipients, string subject, string message)
    {
        if (String.IsNullOrWhiteSpace(subject))
            throw new ArgumentException(nameof(subject), "subject must be provided");

        if (String.IsNullOrWhiteSpace(message))
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

        _notificationContext.Add(notification);

        _notificationContext.ChangeTracker.DetectChanges();
        _log.LogDebug($"{_notificationContext.ChangeTracker.DebugView.LongView}");

        _notificationContext.SaveChanges();

        return notification;
    }
}