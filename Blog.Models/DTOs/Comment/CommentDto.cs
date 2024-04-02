using Blog.Models.DTOs.Account;
using Blog.Models.Entities;

namespace Blog.Models.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }

    public string Content { get; set; } = String.Empty;

    public int? ParentId { get; set; }

    public int PostId { get; set; }

    public UserDto CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}