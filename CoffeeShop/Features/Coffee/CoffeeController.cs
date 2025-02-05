using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Coffee;

[ApiController]
[Route("api/[controller]")]
public class CoffeeController : Controller
{
    private readonly ICoffeeItemService _coffeeService;
    public CoffeeController(ICoffeeItemService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    [HttpGet]
    public async Task<List<CoffeeItem>> GetList()
    {
        var result = await _coffeeService.List();
        return result;
    }

    [HttpGet("{int:id}")]
    public async Task<ActionResult> GetById(int id)
    {

        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Add(ItemDTO item)
    {
        CoffeeItem coffee = new CoffeeItem(item.Name, item.Description, item.Category, item.Price, item.Size, item.PictureUri, new Customization(item.Option, item.Choices), new Availability(item.InStock, item.NextBatchTime));
        var result = await _coffeeService.Create(coffee);
        return Ok();
    }

    [HttpDelete("{int:id}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
        await _coffeeService.Delete(id);
        return Ok();
    }
}
