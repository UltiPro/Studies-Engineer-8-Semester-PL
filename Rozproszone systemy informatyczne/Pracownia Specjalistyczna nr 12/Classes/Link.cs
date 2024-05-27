namespace WebApplication1.Classes;

public class Link
{
    public string LinkUrl {  get; set; }
    public string Rel { get; set; }

    public Link(string LinkUrl, string Rel)
    {
        this.LinkUrl = LinkUrl;
        this.Rel = Rel;
    }
}
