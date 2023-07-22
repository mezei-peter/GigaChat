using Microsoft.AspNetCore.SignalR;
using GigaChat.Services;
using GigaChat.Models;

namespace GigaChat.Hubs;

public class ChatHub : Hub
{
    private readonly GigaChatDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public ChatHub(GigaChatDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task PingAll(string msg)
        => await Clients.All.SendAsync("ReceivePing", msg);

    public async Task AddToChatRoomGroup(string userToken, string roomId)
    {
        User? dummyUser = _jwtService.DecodeUserFromJwt(userToken, "TEST_SECRET");
        if (dummyUser == null)
        {
            return;
        }
        ChatRoom chatRoom = _dbContext.ChatRooms.Where(cRoom => cRoom.Id.Equals(Guid.Parse(roomId))).First();
        ChatRoomMembership? membership = _dbContext.Memberships
          .Where(mShip => mShip.ChatRoom.Id.Equals(chatRoom.Id) && mShip.User.Id.Equals(dummyUser.Id))
          .FirstOrDefault();
        if (membership != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
