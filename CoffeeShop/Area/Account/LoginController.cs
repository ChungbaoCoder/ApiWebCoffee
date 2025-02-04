using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Area.Account;

[Area("Account")]
[ApiController]
[Route("api/Account/[controller]")]
public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}
