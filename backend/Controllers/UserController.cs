using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using GigaChat.Models;

namespace GigaChat.Controllers;

public class UserController : Controller
{
    private GigaChatDbContext _dbContext;

    public UserController(GigaChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_dbContext.Users);
    }
}
