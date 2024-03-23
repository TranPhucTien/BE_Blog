using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("comments")]
public class Comment : BaseEntity
{
    public string Content { get; set; } = String.Empty;
    
    public int? ParentId { get; set; }
    
    public int Left { get; set; }
    
    public int Right { get; set; }
    
    public int PostId { get; set; }
    
    public int AuthorId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
}