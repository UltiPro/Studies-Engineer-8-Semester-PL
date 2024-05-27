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

    private static List<Comment> comments = new List<Comment> {
        new Comment(0, "Jakiś komentarz"),
        new Comment(0, "Coś tam hehe"),
        new Comment(1, "Tak"),
        new Comment(2, "Nie")
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

        message.links = new List<Link>()
        {
            new Link(HttpContext.Request.GetDisplayUrl(),"self"),
            new Link(HttpContext.Request.GetDisplayUrl()+"/comments","comments"),
        };

        return Ok(message);
    }

    [HttpGet("{id}/comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetComments(int id)
    {
        var commentsList = comments.Where(c => c.CommentId == id).ToList();
        if (commentsList is null || commentsList.Count == 0) return NotFound("Comments not found.");
        return Ok(commentsList);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Post(PostMessage postMessage)
    {
        int id = messages.Last().Id + 1;

        messages.Add(new Message(id, postMessage));

        Response.Headers.Append("X-Location", HttpContext.Request.GetDisplayUrl() + "/" + id);

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

    [HttpGet("query")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetQuery([FromQuery] int id) => Ok(id);

    [HttpGet("header")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetHeader([FromHeader] int id) => Ok(id);

    [HttpPost("matrix")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetMatrix([FromBody] params bool[] bools) => Ok(bools);

    [HttpGet("context")]
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

    [HttpGet("startWith")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult GetString(string startWith)
        => Ok(messages.Where(message => message.MessageText.StartsWith(startWith, StringComparison.OrdinalIgnoreCase)));
}