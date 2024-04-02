using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTOs.Comment;

public class CreateCommentDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public int PostId { get; set; }

    public int? ParentId { get; set; }
}