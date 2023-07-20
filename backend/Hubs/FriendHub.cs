using GigaChat.Models;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Hubs;

public class FriendHub : Hub
{
    private readonly GigaChatDbContext _dbContext;

    public FriendHub(GigaChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Obsolete]
    private static IDictionary<string, object> DecodeJwt(string jwt)
    {
        try
        {
            return JwtBuilder.Create()
                            .WithAlgorithm(new HMACSHA256Algorithm())
                            .WithSecret("TEST_SECRET")
                            .MustVerifySignature()
                            .Decode<IDictionary<string, object>>(jwt);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static string GetUserNameFromIDictionary(IDictionary<string, object> dict)
    {
        string userName = dict["UserName"]?.ToString() ?? "";
        if (userName == "")
        {
            throw new Exception("No valid username found in Dictionary.");
        }
        return userName;
    }

    [Obsolete]
    public async Task AddToSelfGroup(string userToken)
    {
        IDictionary<string, object> details = DecodeJwt(userToken);
        string userName = GetUserNameFromIDictionary(details);
        await Groups.AddToGroupAsync(Context.ConnectionId, userName);
    }
}