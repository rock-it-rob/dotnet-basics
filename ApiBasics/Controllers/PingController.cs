using Microsoft.AspNetCore.Mvc;

namespace ApiBasics.Controllers;

/// <summary>
/// A controller whose only purpose is for diagnostic verifications.
/// </summary>
[Route("[controller]")]
public class PingController : AbstractApiController
{
    private readonly ILogger<PingController> _log;

    public PingController(ILogger<PingController> log) =>
        _log = log;

    [HttpGet]
    public Dictionary<string, string> get()
    {
        _log.LogInformation("Responding with diagnostics");
        return new Dictionary<string, string>() { ["status"] = "Ok" };
    }
}