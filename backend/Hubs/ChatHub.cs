using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Hubs;

public class ChatHub : Hub
{
    public async Task PingAll(string msg)
        => await Clients.All.SendAsync("ReceivePing", msg);
}