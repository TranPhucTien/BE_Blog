using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("bookmarks")]
public class BookMark
{
    public string UserId { get; set; } = String.Empty;

    public User User { get; set; } = null!;

    public int PostId { get; set; }

    public Post Post { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}