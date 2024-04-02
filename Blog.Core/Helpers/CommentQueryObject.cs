using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Helpers;

public class CommentQueryObject : QueryObject
{
    [Required]
    public int PostId { get; set; }
    public string? ParentId { get; set; }
}