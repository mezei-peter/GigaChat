using GigaChat.Models;

namespace GigaChat.Controllers.Dtos;

public record ChatMessageDto(Guid Id, PublicUserDetails Author, string Message, DateTime DateTime)
{
    public static ChatMessageDto FromChatMessage(ChatMessage Message)
    {
        return new ChatMessageDto(Message.Id, PublicUserDetails.FromUser(Message.Author),
            Message.Message, Message.DateTime);
    }
}
