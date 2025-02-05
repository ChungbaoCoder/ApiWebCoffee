using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    [HttpPost]
    public IActionResult Register()
    {
        return Ok("User");
    }
}
