using System.ComponentModel.DataAnnotations;

namespace GigaChat.Models;

public class ChatRoom
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public ChatRoomType Type { get; set; }
}
