using Microsoft.EntityFrameworkCore;

namespace GigaChat.Models;

public class GigaChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<ChatRoomMembership> Memberships { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public GigaChatDbContext(DbContextOptions<GigaChatDbContext> options) : base(options)
    {
    }
}
