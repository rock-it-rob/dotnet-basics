using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkBasics.Notify.Data;
using EntityFrameworkBasics.Notify.Data.Repository;
using EntityFrameworkBasics.Notify.Data.Model;

namespace EntityFrameworkBasics.Test;

public class Tests : AbstractRepositoryTest
{
    private static readonly string SUBJECT = "test subject";

    private NotificationContext? _notificationContext;
    private NotificationRepository? _repository;

    [SetUp]
    public void Create()
    {
        // Create the context and repository.
        _notificationContext = new NotificationContext(new DbContextOptions<NotificationContext>(), DbConfiguration, new NullLogger<NotificationContext>());
        _repository = new NotificationRepository(_notificationContext, new NullLogger<NotificationRepository>());
    }

    [TearDown]
    public void DisposeContext()
    {
        _notificationContext?.Dispose();
    }

    private long CreateNotification()
    {
        using var command = _notificationContext!
            .Database
            .GetDbConnection()
            .CreateCommand();
        command.CommandText = "insert into notifications(subject, updated) values($1, $2) returning id";

        var p1 = command.CreateParameter();
        p1.Value = SUBJECT;
        command.Parameters.Add(p1);

        var p2 = command.CreateParameter();
        p2.Value = DateTime.UtcNow;
        command.Parameters.Add(p2);

        return (long)command.ExecuteScalar()!;
    }

    [Test]
    public void TestCreate()
    {
        using var transaction = _notificationContext!.Database.BeginTransaction();

        var n = new Notification { Subject = "test subject", NotificationMessage = new NotificationMessage { Message = "Test message" } };
        _repository!.Create(n);
        _repository!.SaveChanges();

        using var cmd = _notificationContext.Database.GetDbConnection().CreateCommand();
        cmd.CommandText = """
            select
                n.subject,
                m.message
            from
                notifications n
            join notification_messages m on
                n.id = m.notification_id
            where
                n.id = $1
        """;
        var p1 = cmd.CreateParameter();
        p1.Value = n.Id;
        cmd.Parameters.Add(p1);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read())
            throw new DataException("Missing data for Notification");

        var subject = reader.GetFieldValue<string>(0);
        var message = reader.GetFieldValue<string>(1);

        Assert.That(subject, Is.EqualTo(n.Subject));
        Assert.That(message, Is.EqualTo(n.NotificationMessage.Message));
    }

    [Test]
    public void TestRead()
    {
        using var transaction = _notificationContext!.Database.BeginTransaction();

        long id = CreateNotification();
        TestContext.Progress.WriteLine($"Created notification: {id}");

        Notification n = _repository!.Read(id);
        Assert.IsNotNull(n);
    }

    [Test]
    public void TestUpdate()
    {
        using var tx = _notificationContext!.Database.BeginTransaction();

        const string sub = "Updated subject";
        long id = CreateNotification();

        var n = (from notification in _notificationContext.Notifications
                 where notification.Id == id
                 select notification)
            .Include(n => n.NotificationMessage)
            .AsNoTracking()
            .First();

        n.Subject = sub;
        _repository!.Update(n);

        _repository.SaveChanges();
        _notificationContext.ChangeTracker.Clear();

        string subject = _notificationContext.Notifications!
            .FromSql($"select subject from notifications where id = {id}")
            .Select(n => n.Subject)
            .First();

        Assert.That(subject, Is.EqualTo(sub));
    }
}