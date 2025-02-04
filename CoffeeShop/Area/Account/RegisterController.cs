using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Area.Account;

[Area("Account")]
[ApiController]
[Route("api/Account/[controller]")]
public class RegisterController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
}
