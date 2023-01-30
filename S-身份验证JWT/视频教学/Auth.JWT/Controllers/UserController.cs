namespace Auth.JWT.Controllers;
using System.Security.Claims;
using Auth.JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#nullable disable

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("Admins")]
    [Authorize(Roles = "Administrator")]
    public IActionResult AdminsEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.GivenName}, you are {currentUser.Role}");
    }

    [HttpGet("Sellers")]
    [Authorize(Roles = "Seller")]
    public IActionResult SellersEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.GivenName}, you are {currentUser.Role}");
    }


    [HttpGet("AdminsAndSellers")]
    [Authorize(Roles = "Administrator,Seller")]
    public IActionResult AdminsAndSellersEndpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi {currentUser.GivenName}, you are {currentUser.Role}");
    }

    [HttpGet("Public")]
    public IActionResult Public()
    {
        return Ok("Hi, you're here on the public");
    }

    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new User
            {
                UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName).Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname).Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role).Value
            };
        }
        return null;
    }
    // public User GetCurrentUser()
    // {
    //     var identity = HttpContext.User.Identity as ClaimsIdentity;

    //     if (identity != null)
    //     {
    //         var userClaim = identity.Claims;

    //         var user = new User()
    //         {
    //             UserName = userClaim.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
    //             Email = userClaim.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
    //             GivenName = userClaim.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
    //             Surname = userClaim.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
    //             Role = userClaim.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
    //         };
    //         return user;
    //     }
    //     return null;
    // }
}