﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models.Entities;

[Table("post_tags")]
public class PostTag
{
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}