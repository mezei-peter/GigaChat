using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using GigaChat.Models;
using GigaChat.Controllers.Dtos;
using JWT.Builder;
using JWT.Algorithms;
using System.Security.Cryptography;

namespace GigaChat.Controllers;

public class UserController : Controller
{
    private readonly GigaChatDbContext _dbContext;

    public UserController(GigaChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_dbContext.Users);
    }

    [HttpGet]
    public IActionResult GetByJwt(string id)
    {
        string userDetails = JwtBuilder.Create()
                                        .WithAlgorithm(new HMACSHA256Algorithm())
                                        .WithSecret("TEST_SECRET")
                                        .MustVerifySignature()
                                        .Decode(id);
        return Ok(userDetails);
    }

    [HttpGet]
    public IActionResult Login([FromBody] NewUser user)
    {
        //TODO: Check if login credentials are valid
        string token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("UserName", user.UserName)
                      .WithSecret("TEST_SECRET")
                      .Encode();
        return Ok(token);
    }

    [HttpPost]
    public IActionResult PostNewUser([FromBody] NewUser newUser)
    {
        string userName = newUser.UserName;
        string password = newUser.Password;
        User user = new() { Id = Guid.NewGuid(), UserName = userName, Password = password };
        _dbContext.Add(user);
        _dbContext.SaveChanges();
        return Ok(user);
    }
}
