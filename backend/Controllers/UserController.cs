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

    [HttpGet, Route("/User/GetByJwt/{token}")]
    [Obsolete]
    public IActionResult GetByJwt(string token)
    {
        try
        {
            IDictionary<string, object> details = JwtBuilder.Create()
                                            .WithAlgorithm(new HMACSHA256Algorithm())
                                            .WithSecret("TEST_SECRET")
                                            .MustVerifySignature()
                                            .Decode<IDictionary<string, object>>(token);
            PublicUserDetails publicUserDetails = new(Guid.Parse(details["Id"]?.ToString() ?? ""), details["UserName"]?.ToString() ?? "");
            return Ok(publicUserDetails);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpPost]
    [Obsolete]
    public IActionResult Login([FromBody] UsernameAndPassword userCredentials)
    {
        User user = _dbContext.Users
                    .Where(user => userCredentials.UserName == user.UserName && userCredentials.Password == user.Password)
                    .First();
        string token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("Id", user.Id.ToString())
                      .AddClaim("UserName", user.UserName)
                      .WithSecret("TEST_SECRET")
                      .Encode();
        return Ok(token);
    }

    [HttpPost]
    public IActionResult PostNewUser([FromBody] UsernameAndPassword newUser)
    {
        string userName = newUser.UserName;
        string password = newUser.Password;
        User user = new() { UserName = userName, Password = password };
        _dbContext.Add(user);
        _dbContext.SaveChanges();
        return Ok(user);
    }
}
