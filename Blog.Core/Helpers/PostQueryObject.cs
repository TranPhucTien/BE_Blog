namespace Blog.Core.Helpers;

public class PostQueryObject : QueryObject
{
    public string? AuthorId { get; set; }
    public int? TagId { get; set; }
}