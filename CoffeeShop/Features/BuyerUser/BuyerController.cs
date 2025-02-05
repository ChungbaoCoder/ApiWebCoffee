using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.BuyerUser;

public class BuyerController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
