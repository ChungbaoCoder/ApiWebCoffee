using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Display;

[ApiController]
[Route("api/[controller]")]
public class DisplayController : Controller
{
    [HttpGet]
    public IActionResult GetAllProduct()
    {
        return Ok("Hello");
    }
}
