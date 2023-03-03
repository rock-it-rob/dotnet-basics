using Microsoft.AspNetCore.Mvc;
using ApiBasics.Model;
namespace ApiBasics.Controllers;

[Route("[controller]")]
public class NotificationController : AbstractApiController
{
    private readonly static List<Notification> _notifications = new List<Notification>();

    private readonly ILogger<NotificationController> _log;

    static NotificationController()
    {
        _notifications.Add(new Notification { Id = 1, Subject = "Notification One" });
        _notifications.Add(new Notification { Id = 2, Subject = "Notification Two" });
        _notifications.Add(new Notification { Id = 2, Subject = "Notification Three" });
    }

    public NotificationController(ILogger<NotificationController> log) =>
        _log = log;

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Notification> get(int id)
    {
        _log.LogInformation($"Received request for notification with id: {id}");

        try
        {
            return (from notification in _notifications
                    where notification.Id == id
                    select notification).Single();
        }
        catch (InvalidOperationException e)
        {
            _log.LogError($"Request for {id} failed: {e.Message}");
            return NotFound();
        }
    }
}