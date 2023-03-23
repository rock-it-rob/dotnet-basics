/// <summary>Provides CRUD for Notification objects</summary>
using EntityFrameworkBasics.Data;
using EntityFrameworkBasics.Notify.Data.Model;

namespace EntityFrameworkBasics.Notify.Data.Repository;

public interface INotificationRepository : IRepository<Notification, long>
{
}