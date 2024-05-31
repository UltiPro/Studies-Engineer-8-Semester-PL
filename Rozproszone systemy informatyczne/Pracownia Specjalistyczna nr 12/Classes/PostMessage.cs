#pragma warning disable

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Classes;

public class PostMessage
{
    [Required]
    public string Author { get; set; }
    [Required]
    public string MessageText { get; set; }
}