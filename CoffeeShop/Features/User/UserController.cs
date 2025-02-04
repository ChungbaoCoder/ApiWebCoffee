using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("User");
    }
}
