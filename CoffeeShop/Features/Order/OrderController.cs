using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Order;

[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Order");
    }
}
