namespace WebApplication1.Classes;

public class Message
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string MessageText { get; set; }
    public DateTime DateTime { get; set; }
    public List<Link>? links { get; set; }

    public Message(int Id, string Author, string MessageText, DateTime DateTime)
    {
        this.Id = Id;
        this.Author = Author;
        this.MessageText = MessageText;
        this.DateTime = DateTime;
    }

    public Message(int Id, PostMessage postMessage)
    {
        this.Id = Id;
        Author = postMessage.Author;
        MessageText = postMessage.MessageText;
        DateTime = DateTime.Now;
    }
}