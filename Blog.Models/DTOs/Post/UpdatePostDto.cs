using System.ComponentModel.DataAnnotations;
using Blog.Models.Validations;

namespace Blog.Models.DTOs.Post;

public class UpdatePostDto
{
    [Required]
    [MaxLength(200, ErrorMessage = "Title don't over 200 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(200, ErrorMessage = "Description don't over 200 characters")]
    public string Desc { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    
    [Required]
    public List<int> TagIds { get; set; } = [];
}