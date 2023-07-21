using System.ComponentModel.DataAnnotations;

namespace GigaChat.Models;

public class ChatRoom
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    [Required]
    public ChatRoomType Type { get; set; }
}
