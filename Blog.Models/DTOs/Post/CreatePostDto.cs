using System.ComponentModel.DataAnnotations;
using Blog.Models.Validations;

namespace Blog.Models.DTOs.Post;

public class CreatePostDto
{
    [Required]
    [MaxLength(200, ErrorMessage = "Title don't over 200 characters")]
    public string Title { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(200, ErrorMessage = "Description don't over 200 characters")]
    public string Desc { get; set; } = String.Empty;

    [Required]
    public string Content { get; set; } = String.Empty;
    
    public List<int> TagIds { get; set; } = new List<int>();
    
    [Required]
    [CurrentDateOrLater("PublishedAt must be current date or later")]
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    
}