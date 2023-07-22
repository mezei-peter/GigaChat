using GigaChat.Models;
using GigaChat.Services;
using GigaChat.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Controllers;

public class ChatController : Controller
{
    private readonly GigaChatDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public ChatController(GigaChatDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    [HttpGet, Route("/Chat/GetDirectChatRoom/{userToken}/{friendId}")]
    public IActionResult GetDirectChatRoom(string userToken, string friendId)
    {
        try
        {

            User? dummyUser = _jwtService.DecodeUserFromJwt(userToken, "TEST_SECRET");
            if (dummyUser == null)
            {
                return Unauthorized();
            }
            Guid friendGuid = Guid.Parse(friendId);
            ChatRoom? chatRoom = _dbContext.ChatRooms
              .Join(_dbContext.Memberships,
                  cRoom => cRoom.Id,
                  mship => mship.ChatRoom.Id,
                  (cRoom, mShip) => new
                  {
                      ChatRoom = cRoom,
                      Membership = mShip
                  })
              .Where(t => t.ChatRoom.Type == ChatRoomType.DIRECT && (t.Membership.User.Id.Equals(dummyUser.Id) || t.Membership.User.Id.Equals(friendGuid)))
              .GroupBy(
                  set => set.ChatRoom,
                  (cRoom, entries) => new
                  {
                      Count = entries.Count(),
                      ChatRoom = cRoom
                  })
              .Where(set => set.Count == 2)
                .Select(set => set.ChatRoom)
                .FirstOrDefault();
            if (chatRoom == null)
            {
                return NotFound();
            }
            ChatMessageDto[] messages = _dbContext.ChatMessages
              .Where(msg => msg.ChatRoom.Id.Equals(chatRoom.Id))
              .Select(msg => ChatMessageDto.FromChatMessage(msg))
              .ToArray();
            return Ok(new ChatRoomDto(chatRoom, messages));
        }
        catch (FormatException)
        {
            return BadRequest();
        }
    }
}
