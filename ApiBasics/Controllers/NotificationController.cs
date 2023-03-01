using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        foreach (var n in _notifications)
        {
            if (n.Id == id)
                return n;
        }

        return NotFound();
    }
}