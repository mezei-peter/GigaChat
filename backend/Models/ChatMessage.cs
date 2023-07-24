using System.ComponentModel.DataAnnotations;

namespace GigaChat.Models;

public class ChatMessage
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public ChatRoom ChatRoom { get; set; } = null!;

    [Required]
    public User Author { get; set; } = null!;

    [Required]
    public string Message { get; set; } = null!;

    [Required]
    public DateTime DateTime { get; set; } 
}
