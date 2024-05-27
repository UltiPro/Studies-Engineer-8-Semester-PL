namespace WebApplication1.Classes;

public class Comment
{
    public int CommentId { get; set; }
    public string CommentText { get; set; }

    public Comment(int CommentId, string CommentText)
    {
        this.CommentId = CommentId;
        this.CommentText = CommentText;
    }
}