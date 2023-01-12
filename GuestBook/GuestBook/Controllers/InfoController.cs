using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.Controllers;

[Route("api/info")]
public class InfoController : ControllerBase
{
    [HttpGet("version")]
    public async Task<ActionResult<string>> GetVersionAsync()
    {
        var version = await System.IO.File.ReadAllTextAsync(@".version");
        return version;
    }
    
    [HttpGet("host")]
    public async Task<ActionResult<string>> GetHostAsync()
    {
        var hostName = Dns.GetHostName();
        return hostName;
    }
}

