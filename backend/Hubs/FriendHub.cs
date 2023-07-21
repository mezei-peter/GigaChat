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
        if (_dbContext.FriendShips
            .Where(f => f.Proposer.Id.Equals(senderId) && f.Accepter.Id.Equals(receiver.Id))
            .ToArray().Length == 0)
        {
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

    [Obsolete]
    public async Task AcceptFriendRequest(string accepterToken, string friendId)
    {
        Guid accepterId = Guid.Parse(GetDataFromIDictionary(DecodeJwt(accepterToken), "Id"));
        User accepter = _dbContext.Users.Where(u => u.Id.Equals(accepterId)).First();
        User friend = _dbContext.Users.Where(u => u.Id.Equals(Guid.Parse(friendId))).First();
        if (accepter.Id.Equals(friend.Id))
        {
            return;
        }
        FriendShip existingFriendship = _dbContext.FriendShips
            .Where(f => f.Accepter.Id.Equals(accepter.Id) && f.Proposer.Id.Equals(friend.Id)).FirstOrDefault();
        if (existingFriendship != null && !existingFriendship.IsAccepted)
        {
            existingFriendship.IsAccepted = true;
            existingFriendship.DateOfAcceptance = DateTime.Now;
            _dbContext.SaveChanges();
            await Clients.Group(accepter.UserName).SendAsync("AddFriend", friend.Id.ToString(), friend.UserName);
            await Clients.Group(friend.UserName).SendAsync("AddFriend", accepter.Id.ToString(), accepter.UserName);
        }
    }
}
