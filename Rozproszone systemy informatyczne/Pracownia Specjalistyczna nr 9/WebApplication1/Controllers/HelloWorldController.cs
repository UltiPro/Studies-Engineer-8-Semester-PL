using System.Net;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.models;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public string HelloWorld(string nick)
    {
        return $"Hello {nick}";
    }

    [HttpGet("/xml")]
    [Produces("application/xml")]
    public IActionResult GetUser()
    {
        var user = new User { Id = 1, Name = "Steve James" };
        return Ok(user);
    }

    [HttpGet("/json")]
    [Produces("application/json")]
    public IActionResult GetUser2()
    {
        var user = new User { Id = 1, Name = "Steve James" };
        return Ok(user);
    }
}
