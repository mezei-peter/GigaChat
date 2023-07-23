using GigaChat.Models;

namespace GigaChat.Controllers.Dtos;

public record ChatMessageDto(Guid Id, PublicUserDetails Author, string Message, DateTime DateTime)
{
    public static ChatMessageDto FromChatMessage(ChatMessage message)
    {
        return new ChatMessageDto(message.Id, PublicUserDetails.FromUser(message.Author),
            message.Message, message.DateTime);
    }
}
