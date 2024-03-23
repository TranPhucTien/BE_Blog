using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

public abstract class BaseEntity {
    public int Id { get; set; }
}