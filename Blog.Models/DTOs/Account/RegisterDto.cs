using System.ComponentModel.DataAnnotations;

namespace Blog.Models.DTOs.Account;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}