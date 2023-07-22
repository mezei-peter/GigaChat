using GigaChat.Models;

namespace GigaChat.Controllers.Dtos;

public record ChatRoomDto(ChatRoom Room, ICollection<ChatMessage> Messages)
{
}
