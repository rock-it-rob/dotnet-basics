using Microsoft.AspNetCore.Mvc;
using ApiBasics.Model;

namespace ApiBasics.Controllers;

/// <summary>
/// A controller whose only purpose is for diagnostic verifications.
/// </summary>
[Route("[controller]")]
public class PingController : AbstractApiController
{
    private readonly ILogger<PingController> _log;
    private readonly IHostEnvironment _environment;

    public PingController(ILogger<PingController> log, IHostEnvironment environment) =>
        (_log, _environment) = (log, environment);

    [HttpGet]
    public Diagnostic get()
    {
        _log.LogInformation("Responding with diagnostics");

        return new Diagnostic { Environment = _environment.EnvironmentName };
    }
}