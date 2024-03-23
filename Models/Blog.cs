using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Blog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Desc { get; set; }

    public string Content { get; set; }
    
    public int AuthorId { get; set; }
    
    public int Views { get; set; } = 0;
    
    public int Bookmarks { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}