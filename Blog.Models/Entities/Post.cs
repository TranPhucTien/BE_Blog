using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("posts")]
public class Post : BaseEntity
{
    public string Title { get; set; } = String.Empty;
    
    public string Desc { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
    
    public string AuthorId { get; set; }
    
    public User Author { get; set; }
    
    public int Views { get; set; }
    
    public int Bookmarks { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}