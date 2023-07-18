using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using GigaChat.Models;
using GigaChat.Controllers.Dtos;

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

    [HttpPost]
    public IActionResult PostNewUser([FromBody] NewUser newUser)
    {
        string userName = newUser.UserName;
        string password = newUser.Password;
        User user = new User { Id = Guid.NewGuid(), UserName = userName, Password = password };
        _dbContext.Add(user);
        _dbContext.SaveChanges();
        return Ok(user);
    }
}
