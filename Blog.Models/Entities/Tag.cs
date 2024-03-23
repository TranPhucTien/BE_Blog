using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("tags")]
public class Tag : BaseEntity
{
    public string Name { get; set; } = String.Empty;
}