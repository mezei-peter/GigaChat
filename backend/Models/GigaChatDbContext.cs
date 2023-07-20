using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GigaChat.Models;

public class GigaChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }

    public GigaChatDbContext(DbContextOptions<GigaChatDbContext> options) : base(options)
    {
    }
}
