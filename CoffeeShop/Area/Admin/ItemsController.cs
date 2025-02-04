using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Area.Admin;

[Area("Admin")]
[ApiController]
[Route("api/Admin/[controller]")]
public class ItemsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Get item");
    }
}
