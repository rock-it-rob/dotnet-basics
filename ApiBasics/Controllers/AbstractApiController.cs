using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ApiBasics.Controllers;

/// <summary>
/// Base class for all controllers in this app.
/// </summary>
/// The default media type for all controllers is json.
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class AbstractApiController : ControllerBase
{
}