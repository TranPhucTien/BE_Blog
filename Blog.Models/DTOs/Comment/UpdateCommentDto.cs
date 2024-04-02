using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTOs.Comment;

public class UpdateCommentDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Content { get; set; }
}