using GigaChat.Models;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.SignalR;
using GigaChat.Services;

namespace GigaChat.Hubs;

public class FriendHub : Hub
{
    private readonly GigaChatDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public FriendHub(GigaChatDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task AddToSelfGroup(string userToken)
    {
        User? user = _jwtService.DecodeUserFromJwt(userToken, "TEST_SECRET");
        if (user != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user.UserName);
        }
    }

    [Obsolete]
    public async Task SendFriendRequest(string senderToken, string receiverUserName)
    {
        User? dummySender = _jwtService.DecodeUserFromJwt(senderToken, "TEST_SECRET");
        if (dummySender == null)
        {
            return;
        }
        User sender = _dbContext.Users.Where(u => u.Id.Equals(dummySender.Id)).First();
        User receiver = _dbContext.Users.Where(u => u.UserName == receiverUserName).First();

        if (sender.Id.Equals(receiver.Id))
        {
            return;
        }
        FriendShip? friendShip = _dbContext.FriendShips
            .Where(f => f.Proposer.Id.Equals(sender.Id) && f.Accepter.Id.Equals(receiver.Id))
            .FirstOrDefault();
        FriendShip? reverseFriendship = _dbContext.FriendShips
            .Where(f => f.Proposer.Id.Equals(receiver.Id) && f.Accepter.Id.Equals(sender.Id))
            .FirstOrDefault();
        if (friendShip == null && reverseFriendship == null)
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
        else if (reverseFriendship != null && !reverseFriendship.IsAccepted)
        {
            reverseFriendship.IsAccepted = true;
            reverseFriendship.DateOfAcceptance = DateTime.Now;
            ChatRoom chatRoom = _dbContext.ChatRooms.Add(new() { Type = ChatRoomType.DIRECT }).Entity;
            _dbContext.Memberships.Add(new() { ChatRoom = chatRoom, User = receiver });
            _dbContext.Memberships.Add(new() { ChatRoom = chatRoom, User = sender });
            _dbContext.SaveChanges();
            await Clients.Group(receiver.UserName).SendAsync("AddFriend", sender.Id.ToString(), sender.UserName);
            await Clients.Group(sender.UserName).SendAsync("AddFriend", receiver.Id.ToString(), receiver.UserName);
        }
    }

    [Obsolete]
    public async Task AcceptFriendRequest(string accepterToken, string friendId)
    {
        User? dummyAccepter = _jwtService.DecodeUserFromJwt(accepterToken, "TEST_SECRET");
        if (dummyAccepter == null)
        {
            return;
        }
        User accepter = _dbContext.Users.Where(u => u.Id.Equals(dummyAccepter.Id)).First();
        User friend = _dbContext.Users.Where(u => u.Id.Equals(Guid.Parse(friendId))).First();
        if (accepter.Id.Equals(friend.Id))
        {
            return;
        }
        FriendShip? existingFriendship = _dbContext.FriendShips
            .Where(f => f.Accepter.Id.Equals(accepter.Id) && f.Proposer.Id.Equals(friend.Id)).FirstOrDefault();
        if (existingFriendship != null && !existingFriendship.IsAccepted)
        {
            existingFriendship.IsAccepted = true;
            existingFriendship.DateOfAcceptance = DateTime.Now;
            ChatRoom chatRoom = _dbContext.ChatRooms.Add(new() { Type = ChatRoomType.DIRECT }).Entity;
            _dbContext.Memberships.Add(new() { ChatRoom = chatRoom, User = accepter });
            _dbContext.Memberships.Add(new() { ChatRoom = chatRoom, User = friend });
            _dbContext.SaveChanges();
            await Clients.Group(accepter.UserName).SendAsync("AddFriend", friend.Id.ToString(), friend.UserName);
            await Clients.Group(friend.UserName).SendAsync("AddFriend", accepter.Id.ToString(), accepter.UserName);
        }
    }
}
