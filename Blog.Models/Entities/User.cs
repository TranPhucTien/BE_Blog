﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Entities;

[Table("users")]
public class User : IdentityUser
{
    public List<BookMark> BookMarks { get; set; } = new List<BookMark>();
}