namespace Blog.Models.DTOs.Comment;

public class CreateCommentDto
{
    public string Content { get; set; }
    public int PostId { get; set; }
    public int? ParentId { get; set; }
}