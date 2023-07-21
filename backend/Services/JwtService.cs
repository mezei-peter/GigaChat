using GigaChat.Models;
using JWT.Algorithms;
using JWT.Builder;

namespace GigaChat.Services;

public class JwtService : IJwtService
{
    [Obsolete]
    public User? DecodeUserFromJwt(string jwt, string secret)
    {
        try
        {
            IDictionary<string, object> details = JwtBuilder.Create()
                                    .WithAlgorithm(new HMACSHA256Algorithm())
                                    .WithSecret("TEST_SECRET")
                                    .MustVerifySignature()
                                    .Decode<IDictionary<string, object>>(jwt);
            string userName = details["UserName"]?.ToString() ?? "";
            string idString = details["Id"]?.ToString() ?? "";
            Guid id = Guid.Parse(idString);
            return new User() { UserName = userName, Id = id };
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is FormatException)
        {
            return null;
        }
    }

    [Obsolete]
    public string EncodeUserToJwt(User user, string secret)
    {
        return JwtBuilder.Create()
                        .WithAlgorithm(new HMACSHA256Algorithm())
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                        .AddClaim("Id", user.Id.ToString())
                        .AddClaim("UserName", user.UserName)
                        .WithSecret(secret)
                        .Encode();
    }
}
