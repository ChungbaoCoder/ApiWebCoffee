using System.Net;
using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
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

        if (!buyers.Any())
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách người mua"), HttpStatusCode.NotFound, "Danh sách người mua không tìm thấy."));

        return Ok(new Response<IEnumerable<BuyerUser>>(RequestMessage.Text("Lấy danh sách người mua"), HttpStatusCode.OK, "Trả về danh sách người mua thành công.", buyers));
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

    [HttpPost("{buyerId}/address")]
    public async Task<ActionResult<BuyerUser>> AddAddress(int buyerId, [FromBody] AddAddressRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Thêm địa chỉ cho người mua"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var buyer = await _buyerService.AddAddress(buyerId, new Address(request.Street, request.City, request.State, request.Country));

        if (buyer == null)
            return NotFound(new Response<object>(RequestMessage.Text("Thêm địa chỉ cho người mua bằng id"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return Created(Request.Path, new Response<BuyerUser>(RequestMessage.Text("Thêm địa chỉ cho người mua bằng id"), HttpStatusCode.Created, "Thêm địa chỉ thành công."));
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

    [HttpPut("{buyerId}/address/{id}")]
    public async Task<ActionResult<Address>> UpdateAddress(int buyerId, int addressId, [FromBody] UpdateAddressRequest request)
    {
        var result = await _buyerService.UpdateAddress(buyerId, new Address(request.Street, request.City, request.State, request.Country));

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Cập nhật địa chỉ của người mua"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return Ok(new Response<Address>(RequestMessage.Text("Cập nhật địa chỉ của người mua"), HttpStatusCode.OK, "Cập nhật địa chỉ thành công.", result));
    }

    [HttpPatch("{buyerId}/address/{id}")]
    public async Task<ActionResult<BuyerUser>> SetDefaultAddress(int buyerId, int id)
    {
        var result = await _buyerService.SetDefaultAddress(buyerId, id);

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Cập nhật địa chỉ của người mua"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return Ok(new Response<BuyerUser>(RequestMessage.Text("Cập nhật địa chỉ của người mua"), HttpStatusCode.OK, "Cập nhật địa chỉ thành công.", result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(int id)
    {
        var result = await _buyerService.DeleteBuyer(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa người mua bằng id"), HttpStatusCode.NotFound, "Người mua không tìm thấy."));

        return NoContent();
    }

    [HttpDelete("{buyerId}/address/{id}")]
    public async Task<IActionResult> DeleteAddress(int buyerId, int id)
    {
        var result = await _buyerService.RemoveAddress(buyerId, id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xoá địa chỉ của người mua"), HttpStatusCode.NotFound, "Địa chỉ không tìm thấy."));

        return NoContent();
    }
}
