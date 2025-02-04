using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Basket;

[ApiController]
[Route("api/[controller]")]
public class BasketController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Basket");
    }
}
