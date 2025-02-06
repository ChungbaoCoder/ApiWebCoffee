using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Features.Coffee;
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
            return NotFound(new Response<object>("Danh sách người mua không tìm thấy.", 404));

        return Ok(new Response<List<BuyerUser>>(buyers, "Trả về danh sách người mua thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBuyerById(int id)
    {
        var buyer = await _buyerService.GetBuyerById(id);

        if (buyer == null)
            return NotFound(new Response<object>("người mua không tìm thấy.", 404));

        return Ok(new Response<BuyerUser>(buyer, "Trả về người mua thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateBuyer([FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ.", 400));

        try
        {
            var buyer = await _buyerService.CreateBuyer(request.Name, request.Email);

            if (buyer == null)
                return StatusCode(500, new Response<object>("Lỗi tạo mới người mua.", 500));

            return Ok(new Response<BuyerUser>(buyer, "Tạo mới người dùng thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>($"Sự cố xảy ra: {ex.Message}", 500));
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateBuyer(int buyerId, [FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ."));

        try
        {
            var result = await _buyerService.UpdateBuyer(buyerId, request.Name, request.Email);

            if (result == null)
                return StatusCode(404, new Response<object>("Không thể tìm thấy nguời mua để cập nhật.", 404));

            return Ok(new Response<BuyerUser>(result, "Cập nhật người mua thành công."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>($"Sự cố xảy ra: {ex.Message}", 500));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(int id)
    {
        var result = await _buyerService.DeleteBuyer(id);

        if (result == false)
            return NotFound(new Response<object>("Không thể tìm thấy người mua để xóa.", 404));

        return Ok(new Response<bool>(result, "Người mua được xóa thành công."));
    }
}
