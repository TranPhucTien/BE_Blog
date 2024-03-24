using Blog.Models.DTOs.Account;
using Blog.Models.Entities;

namespace Blog.Models.DTOs.Post;

public class PostDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = String.Empty;
    
    public string Desc { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
    
    public UserDto Author { get; set; } = new UserDto();
    
    public int Views { get; set; } = 0;
    
    public int Bookmarks { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}