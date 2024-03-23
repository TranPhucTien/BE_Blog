namespace Blog.Models.DTOs.Post;

public class PostDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = String.Empty;
    
    public string Desc { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
    
    public string AuthorId { get; set; } = String.Empty;
    
    public int Views { get; set; } = 0;
    
    public int Bookmarks { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}