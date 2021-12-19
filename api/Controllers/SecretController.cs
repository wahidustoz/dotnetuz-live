using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class SecretController : ControllerBase
{

    [HttpGet]
    public IActionResult Secret()
    {
        return Ok("DOTNETUZ ROCKS 🥳");
    }
}