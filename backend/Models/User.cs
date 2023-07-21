using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
}
