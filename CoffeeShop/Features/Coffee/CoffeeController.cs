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
    public async Task<IActionResult> GetListCoffee(int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest(new Response<object>("Số lượng danh sách phải là số dương.", 400));

        var coffeeItems = await _coffeeService.ListItem(page, pageSize);

        if (coffeeItems == null || coffeeItems.Count == 0)
            return NotFound(new Response<object>("Danh sách sản phẩm không tìm thấy.", 404));

        return Ok(new Response<List<CoffeeItem>>(coffeeItems, "Trả về danh sách sản phẩm thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCoffeeItemById(int id)
    {
        var coffeeItem = await _coffeeService.GetById(id);

        if (coffeeItem == null)
            return NotFound(new Response<object>("Sản phẩm không tìm thấy.", 404));

        return Ok(new Response<CoffeeItem>(coffeeItem, "Trả về sản phẩm thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCoffeeItem([FromBody] CoffeeItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ."));

        try
        {
            var coffeeItem = new CoffeeItem(
                request.Name,
                request.Description,
                request.Category,
                request.Price,
                request.Size,
                request.PictureUri,
                new Availability(request.StockQuantity, request.AvailabilityStatus, request.RestockDate),
                new Customization(request.MilkType, request.SugarLevel, request.Temperature, request.Topping, request.Flavor)
            );

            var result = await _coffeeService.CreateItem(coffeeItem);

            if (result == null)
                return StatusCode(500, new Response<object>("Xảy ra sự cố khi tạo mới sản phẩm.", 500));

            return Ok(new Response<CoffeeItem>(result, "Sản phẩm tạo ra thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>($"Sự cố xảy ra: {ex.Message}", 500));
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateCoffeeItem([FromBody] CoffeeItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ."));

        try
        {
            var coffeeItem = new CoffeeItem(
                request.Name,
                request.Description,
                request.Category,
                request.Price,
                request.Size,
                request.PictureUri,
                new Availability(request.StockQuantity, request.AvailabilityStatus, request.RestockDate),
                new Customization(request.MilkType, request.SugarLevel, request.Temperature, request.Topping, request.Flavor)
            );

            var result = await _coffeeService.UpdateItem(coffeeItem);

            if (result == null)
                return StatusCode(404, new Response<object>("Không thể tìm thấy sản phẩm để cập nhật.", 404));

            return Ok(new Response<CoffeeItem>(result, "Cập nhật sản phẩm thành công."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>($"Sự cố xảy ra: {ex.Message}", 500));
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCoffeeItem(int id)
    {
        var result = await _coffeeService.DeleteItem(id);

        if (result == false)
            return NotFound(new Response<object>("Không thể tìm thấy sản phẩm để xóa.", 404));

        return Ok(new Response<bool>(result, "Sản phẩm được xóa thành công."));
    }
}
