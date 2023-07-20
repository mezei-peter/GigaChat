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

    public async Task PingAll(string msg)
        => await Clients.All.SendAsync("ReceivePing", msg);

    [Obsolete]
    public async Task AddToSelfGroup(string userToken)
    {
        IDictionary<string, object> details = JwtBuilder.Create()
                                            .WithAlgorithm(new HMACSHA256Algorithm())
                                            .WithSecret("TEST_SECRET")
                                            .MustVerifySignature()
                                            .Decode<IDictionary<string, object>>(userToken);
        string userName = details["UserName"]?.ToString() ?? "";
        if (userName == "")
        {
            return;
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        Console.WriteLine(userName + " added to their self-group");
    }
}