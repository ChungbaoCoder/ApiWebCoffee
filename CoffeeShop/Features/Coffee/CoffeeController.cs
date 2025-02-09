using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Coffee;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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
            return BadRequest(new Response<object>(RequestMessage.Text("Lấy danh sách cà phê"), "Bad Request", "Số sản phẩm và số trang cần hiển thị phải là số dương."));

        var coffeeItems = await _coffeeService.ListItem(page, pageSize);

        if (coffeeItems == null || coffeeItems.Count == 0)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách cà phê"), "Not Found", "Danh sách sản phẩm không tìm thấy.", 404));

        return Ok(new Response<List<CoffeeItem>>(coffeeItems, RequestMessage.Text("Lấy danh sách cà phê"), "Ok", "Trả về danh sách sản phẩm thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCoffeeItemById(int id)
    {
        var coffeeItem = await _coffeeService.GetById(id);

        if (coffeeItem == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy cà phê bằng id"), "Not Found", "Sản phẩm không tìm thấy.", 404));

        return Ok(new Response<CoffeeItem>(coffeeItem, RequestMessage.Text("Lấy cà phê bằng id"), "Ok", "Trả về sản phẩm thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCoffeeItem([FromBody] CoffeeItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo cà phê"), "Bad Request", "Dữ liệu không hợp lệ."));

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
            return Ok(new Response<CoffeeItem>(result, RequestMessage.Text("Tạo cà phê"), "Created", "Sản phẩm tạo ra thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo cà phê"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateCoffeeItem(int id, [FromBody] CoffeeItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Cập nhật cà phê"), "Bad Request", "Dữ liệu không hợp lệ."));

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

            var result = await _coffeeService.UpdateItem(id, coffeeItem);

            if (result == null)
                return StatusCode(404, new Response<object>(RequestMessage.Text("Cập nhật cà phê"), "Not Found", "Sản phẩm không tìm thấy.", 404));

            return Ok(new Response<CoffeeItem>(result, RequestMessage.Text("Cập nhật cà phê"), "No Content", "Cập nhật sản phẩm thành công.", 204));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Cập nhật cà phê"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCoffeeItem(int id)
    {
        var result = await _coffeeService.DeleteItem(id);

        if (result == false)
            return StatusCode(404, new Response<object>(RequestMessage.Text("Xóa cà phê bằng id"), "Not Found", "Sản phẩm không tìm thấy.", 404));

        return Ok(new Response<object>(RequestMessage.Text("Xóa cà phê bằng id"), "No Content", "Xóa sản phẩm thành công.", 204));
    }
}
