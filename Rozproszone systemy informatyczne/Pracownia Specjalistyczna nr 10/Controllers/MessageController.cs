using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Classes;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private static List<Message> messages = new List<Message>
    {
        new Message(0, "Mark", "Hello!", DateTime.Now),
        new Message(1, "Frank", "Hello!", DateTime.Now),
        new Message(2, "Mark", "What's up?", DateTime.Now),
        new Message(3, "Frank", "Nothing.", DateTime.Now),
        new Message(4, "Mark", "Great.", DateTime.Now)
    };

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Get() => Ok(messages);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Get(int id)
    {
        var message = messages.FirstOrDefault(x => x.Id == id);
        if (message is null) return NotFound("Message not found.");
        return Ok(message);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Post(PostMessage postMessage)
    {
        messages.Add(new Message(messages.Last().Id + 1, postMessage));
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Put(int id, string message)
    {
        var mess = messages.FirstOrDefault(m => m.Id == id);
        if (mess is null) return NotFound("Message not found.");
        mess.MessageText = message;
        return Accepted();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Delete(int id)
    {
        var mess = messages.FirstOrDefault(m => m.Id == id);
        if (mess is null) return NotFound("Message not found.");
        messages.Remove(mess);
        return NoContent();
    }

    [HttpGet("/query")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetQuery([FromQuery] int id) => Ok(id);

    [HttpGet("/header")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetHeader([FromHeader] int id) => Ok(id);

    [HttpPost("/matrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetMatrix([FromBody] params bool[] bools) => Ok(bools);

    [HttpGet("/context")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetContext([FromHeader] int id, [FromQuery] string napis)
    {
        string text = $"{HttpContext.Request.Scheme}" +
        $"://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";

        text += "\n" + HttpContext.Request.GetDisplayUrl();

        var headers = HttpContext.Request.Headers.Select(h => $"{h.Key}: {h.Value}\n").ToList();

        foreach (var header in headers)
            text += $"\n{header}";

        return Ok(text);
    }
}