using System.Net;
using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Buyer;

[ApiController]
[Route("api/buyer")]
public class BuyerController : Controller
{
    private readonly IBuyerService _buyerService;
    public BuyerController(IBuyerService buyerService)
    {
        _buyerService = buyerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuyerUser>>> GetListBuyer()
    {
        var buyers = await _buyerService.ListBuyer();

        if (buyers == null || buyers.Count == 0)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách người mua"), HttpStatusCode.NotFound, "Danh sách người mua không tìm thấy."));

        return Ok(new Response<List<BuyerUser>>(RequestMessage.Text("Lấy danh sách người mua"), HttpStatusCode.OK, "Trả về danh sách người mua thành công.", buyers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuyerUser>> GetBuyerById(int id)
    {
        var buyer = await _buyerService.GetBuyerById(id);

        if (buyer == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy người mua bằng id"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return Ok(new Response<BuyerUser>(RequestMessage.Text("Lấy người mua bằng id"), HttpStatusCode.OK, "Trả về người mua thành công.", buyer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBuyer([FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo người mua"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var result = await _buyerService.CreateBuyer(request.Name, request.Email, request.PhoneNum);
            return Created(Request.Path, new Response<BuyerUser>(RequestMessage.Text("Tạo người mua"), HttpStatusCode.Created, "Người mua tạo ra thành công.", result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo người mua"), HttpStatusCode.InternalServerError, $"Lỗi: {ex.Message}."));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBuyer(int id, [FromBody] BuyerRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Cập nhật người mua"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var result = await _buyerService.UpdateBuyer(id, request.Name, request.Email, request.PhoneNum);

            if (result == null)
                return NotFound(new Response<object>(RequestMessage.Text("Cập nhật người mua"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

            return Ok(new Response<BuyerUser>(RequestMessage.Text("Cập nhật người mua"), HttpStatusCode.OK, "Cập nhật người mua thành công.", result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Cập nhật người mua"), HttpStatusCode.InternalServerError, $"Lỗi: {ex.Message}."));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(int id)
    {
        var result = await _buyerService.DeleteBuyer(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa người mua bằng id"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return NoContent();
    }
}
