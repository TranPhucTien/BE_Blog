using Blog.Models.DTOs.Post;

namespace Blog.Models.DTOs.Bookmark;

public class BookmarkDto
{
    public int Id { get; set; }

    public PostDto Post { get; set; }
    
}