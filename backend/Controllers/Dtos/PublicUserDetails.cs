using GigaChat.Models;

namespace GigaChat.Controllers.Dtos;

public record PublicUserDetails(Guid Id, string UserName)
{
    public static PublicUserDetails FromUser(User user)
    {
        return new PublicUserDetails(user.Id, user.UserName);
    }
}
