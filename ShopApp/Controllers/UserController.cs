using Microsoft.AspNetCore.Mvc;
using Shop.App.Filters;
using Shop.Domain.Models;

namespace Shop.App.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    [HttpPost("register")]
   
    [UserCheckFilter]
    public IActionResult Register([FromBody] User user)
    {
        return Ok(user);
    }
}