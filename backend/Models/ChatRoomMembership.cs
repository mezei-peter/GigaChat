using System.ComponentModel.DataAnnotations;

namespace GigaChat.Models;

public class ChatRoomMembership
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public ChatRoom ChatRoom { get; set; } = null!;

    [Required]
    public User User { get; set; } = null!;
}
