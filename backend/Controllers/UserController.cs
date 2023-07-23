using Microsoft.AspNetCore.Mvc;
using GigaChat.Models;
using GigaChat.Controllers.Dtos;
using GigaChat.Services;

namespace GigaChat.Controllers;

public class UserController : Controller
{
    private readonly GigaChatDbContext _dbContext;
    private readonly IJwtService _jwtService;


    public UserController(GigaChatDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(_dbContext.Users);
    }

    [HttpGet, Route("/User/GetByJwt/{token}")]
    public IActionResult GetByJwt(string token)
    {
        User? user = _jwtService.DecodeUserFromJwt(token, "TEST_SECRET");
        if (user == null)
        {
            return BadRequest();
        }
        return Ok(PublicUserDetails.FromUser(user));
    }

    [HttpPost]
    public IActionResult Login([FromBody] UsernameAndPassword userCredentials)
    {
        User user = _dbContext.Users
            .Where(user => userCredentials.UserName == user.UserName && userCredentials.Password == user.Password)
            .First();
        string token = _jwtService.EncodeUserToJwt(user, "TEST_SECRET");
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

    [HttpGet, Route("/User/GetFriendRequests/{token}")]
    public IActionResult GetFriendRequests(string token)
    {
        try
        {
            User? dummyUser = _jwtService.DecodeUserFromJwt(token, "TEST_SECRET");
            if (dummyUser == null)
            {
                return Unauthorized();
            }
            User user = _dbContext.Users.Where(u => dummyUser.Id.Equals(u.Id)).First();
            ICollection<PublicUserDetails> friendRequests = _dbContext.FriendShips
                .Where(f =>
                    f != null && f.Accepter.Id.Equals(user.Id) && f.IsAccepted == false
                )
                .OrderByDescending(f => f.DateOfProposal)
                .Select(f => new PublicUserDetails(f.Proposer.Id, f.Proposer.UserName))
                .ToArray();
            return Ok(friendRequests);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpGet, Route("/User/GetFriends/{token}")]
    public IActionResult GetFriends(string token)
    {
        try
        {
            User? dummyUser = _jwtService.DecodeUserFromJwt(token, "TEST_SECRET");
            if (dummyUser == null)
            {
                return Unauthorized();
            }
            User user = _dbContext.Users
              .Where(u => dummyUser.Id.Equals(u.Id))
              .First();
            ICollection<PublicUserDetails> friends = _dbContext.FriendShips
                .Where(f =>
                    (f.Accepter.Id.Equals(user.Id) || f.Proposer.Id.Equals(user.Id)) && f.IsAccepted == true
                )
                .OrderByDescending(f => f.DateOfAcceptance)
                .Select(f => 
                    f.Proposer.Id.Equals(user.Id)
                    ? new PublicUserDetails(f.Accepter.Id, f.Accepter.UserName)
                    : new PublicUserDetails(f.Proposer.Id, f.Proposer.UserName))
                .ToArray();
            return Ok(friends);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}
