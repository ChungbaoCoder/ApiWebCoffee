using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    [HttpGet]
    public IActionResult GetGuid()
    {
        var existGuid = Request.Cookies["UserGuid"];

        if (string.IsNullOrEmpty(existGuid))
        {
            var newGuid = Guid.NewGuid().ToString();

            Response.Cookies.Append("UserGuid", newGuid, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(30),
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });
            return Ok(newGuid);
        }
        else
        {
            return Ok(existGuid);
        }
    }

    [HttpPost]
    public IActionResult Register()
    {
        return Ok("User");
    }
}
