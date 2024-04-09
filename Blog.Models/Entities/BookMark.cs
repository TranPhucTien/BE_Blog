using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("bookmarks")]
public class BookMark : BaseEntity
{
    public string UserId { get; set; } = String.Empty;

    public int PostId { get; set; }
    public Post Post { get; set; }
}