using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Coffee;

[ApiController]
[Route("api/[controller]")]
public class CoffeeController : Controller
{
    [HttpGet]
    public IActionResult GetAllProduct()
    {
        return Ok("Hello");
    }
}
