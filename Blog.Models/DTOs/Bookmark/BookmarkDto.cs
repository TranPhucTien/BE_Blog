using Blog.Models.DTOs.Post;

namespace Blog.Models.DTOs.Bookmark;

public class BookmarkDto
{
    public PostDto Post { get; set; }

    public DateTime CreatedAt { get; set; }
}