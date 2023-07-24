using GigaChat.Models;

namespace GigaChat.Services;

public interface IJwtService
{
    public string EncodeUserToJwt(User user, string secret);
    public User? DecodeUserFromJwt(string jwt, string secret);
}
