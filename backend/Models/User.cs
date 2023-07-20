using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace GigaChat.Models;

[Index(nameof(UserName), IsUnique = true)]
public class User
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public ICollection<User> Friends { get; set; } = new HashSet<User>();
}
