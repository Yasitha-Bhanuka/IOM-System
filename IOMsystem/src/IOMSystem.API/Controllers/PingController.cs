using Microsoft.AspNetCore.Mvc;

namespace IOMSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Pong! Backend is connected.");
    }
}
