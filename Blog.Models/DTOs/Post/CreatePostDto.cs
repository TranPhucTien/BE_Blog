using System.ComponentModel.DataAnnotations;
using Blog.Models.Validations;

namespace Blog.Models.DTOs.Post;

public class CreatePostDto
{
    [Required]
    [MaxLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
    public string Title { get; set; } = String.Empty;
    
    [Required]
    [MaxLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
    public string Desc { get; set; } = String.Empty;

    [Required]
    public string Content { get; set; } = String.Empty;
    
    [Required]
    [CurrentDateOrLater("Ngày xuất bản")]
    public DateTime PublishedAt { get; set; } = DateTime.Now;
}