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

    private static string GetDataFromIDictionary(IDictionary<string, object> dict, string key)
    {
        string data = dict[key]?.ToString() ?? "";
        if (data == "")
        {
            throw new Exception($"No valid {data} found in Dictionary.");
        }
        return data;
    }

    [Obsolete]
    public async Task AddToSelfGroup(string userToken)
    {
        IDictionary<string, object> details = DecodeJwt(userToken);
        string userName = GetDataFromIDictionary(details, "UserName");
        await Groups.AddToGroupAsync(Context.ConnectionId, userName);
    }

    [Obsolete]
    public async Task SendFriendRequest(string senderToken, string receiverUserName)
    {
        Guid senderId = Guid.Parse(GetDataFromIDictionary(DecodeJwt(senderToken), "Id"));
        User sender = _dbContext.Users.Where(u => u.Id.Equals(senderId)).First();
        User receiver = _dbContext.Users.Where(u => u.UserName == receiverUserName).First();

        if (sender.Id.Equals(receiver.Id))
        {
            return;
        }
        _dbContext.FriendShips.Add(new()
        {
            Proposer = sender,
            Accepter = receiver,
            IsAccepted = false,
            DateOfProposal = DateTime.Now
        });
        _dbContext.SaveChanges();
        await Clients.Group(receiverUserName).SendAsync("ReceiveFriendRequest",
                                                        sender?.Id.ToString(), sender?.UserName);
    }
}