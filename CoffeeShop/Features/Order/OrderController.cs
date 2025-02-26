using System.Net;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Order;

[ApiController]
[Route("api/order")]
[Authorize(AuthenticationSchemes = "JwsToken", Policy = "Moderator")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("buyer/{id}")]
    public async Task<ActionResult<IEnumerable<BuyerOrder>>> GetOrderByBuyerId(int id)
    {
        var orders = await _orderService.GetOrderByBuyerId(id);

        if (!orders.Any())
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), HttpStatusCode.NotFound, "Danh sách hóa đơn của người dùng không tìm thấy."));

        return Ok(new Response<IEnumerable<BuyerOrder>>(RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), HttpStatusCode.OK, $"Trả về danh sách hóa đơn của khách với mã {id} thành công.", orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuyerOrder>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy hóa đơn bằng id"), HttpStatusCode.NotFound, "Hoá đơn không tìm thấy."));

        return Ok(new Response<BuyerOrder>(RequestMessage.Text("Lấy hóa đơn bằng id"), HttpStatusCode.OK, "Trả về hóa đơn thành công.", order));
    }

    [HttpPost("buyer/{id}")]
    public async Task<IActionResult> CreateOrder(int id, [FromBody] List<OrderItemRequest> request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var listOrderItems = request.Select(oi => new OrderItem(oi.ItemVariantId, oi.Price, oi.Quantity)).ToList();

            var result = await _orderService.CreateOrder(id, listOrderItems);

            if (result == null)
                return NotFound(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.NotFound, "Khách hàng không tìm thấy."));

            return Created(Request.Path, new Response<BuyerOrder>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.Created, "Hóa đơn tạo ra thành công.", result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.InternalServerError, $"Lỗi: {ex.Message}."));
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatusRequest request)
    {
        var result = await _orderService.UpdateOrderStatus(id, request.OrderStatus, request.PaymentStatus);

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Cập nhật hóa đơn"), HttpStatusCode.NotFound, "Hóa đơn của khách không tìm thấy."));

        return Ok(new Response<BuyerOrder>(RequestMessage.Text("Cập nhật hóa đơn"), HttpStatusCode.NoContent, "Cập nhật hóa đơn thành công.", result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _orderService.DeleteOrder(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa hóa đơn của người mua"), HttpStatusCode.NotFound, "Hóa đơn không tìm thấy."));

        return NoContent();
    }
}
