using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Buyer;

[ApiController]
[Route("api/[controller]")]
public class BuyerController : Controller
{
    private readonly IBuyerService _buyerService;
    public BuyerController(IBuyerService buyerService)
    {
        _buyerService = buyerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetListBuyer()
    {
        var buyers = await _buyerService.ListBuyer();

        if (buyers == null || buyers.Count == 0)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách người mua"), "Not Found", "Danh sách người mua không tìm thấy.", 404));

        return Ok(new Response<List<BuyerUser>>(buyers, RequestMessage.Text("Lấy danh sách người mua"), "Ok", "Trả về danh sách người mua thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBuyerById(int id)
    {
        var buyer = await _buyerService.GetBuyerById(id);

        if (buyer == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy người mua bằng id"), "Not Found", "Người mua không tìm thấy.", 404));

        return Ok(new Response<BuyerUser>(buyer, RequestMessage.Text("Lấy người mua bằng id"), "Ok", "Trả về người mua thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateBuyer([FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo người mua"), "Bad Request", "Dữ liệu không hợp lệ."));

        try
        {
            var result = await _buyerService.CreateBuyer(request.Name, request.Email);
            return Ok(new Response<BuyerUser>(result, RequestMessage.Text("Tạo người mua"), "Created", "Người mua tạo ra thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo người mua"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateBuyer(int id, [FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Cập nhật người mua"), "Bad Request", "Dữ liệu không hợp lệ."));

        try
        {
            var result = await _buyerService.UpdateBuyer(id, request.Name, request.Email);

            if (result == null)
                return StatusCode(404, new Response<object>(RequestMessage.Text("Cập nhật người mua"), "Not Found", "Người mua không tìm thấy.", 404));

            return Ok(new Response<BuyerUser>(result, RequestMessage.Text("Cập nhật người mua"), "No Content", "Cập nhật người mua thành công.", 204));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Cập nhật người mua"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(int id)
    {
        var result = await _buyerService.DeleteBuyer(id);

        if (result == false)
            return StatusCode(404, new Response<object>(RequestMessage.Text("Xóa người mua bằng id"), "Not Found", "Người mua không tìm thấy.", 404));

        return Ok(new Response<object>(RequestMessage.Text("Xóa người mua bằng id"), "No Content", "Xóa người mua thành công.", 204));
    }
}
