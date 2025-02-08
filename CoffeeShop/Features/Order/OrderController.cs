using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Order;

[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("Buyer/{id}")]
    public async Task<IActionResult> GetOrderByBuyerId(int id)
    {
        var orders = await _orderService.GetOrderByBuyerId(id);

        if (orders == null || orders.Count == 0)
            return BadRequest(new Response<object>(RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), "Bad Request", "Danh sách hóa đơn của người dùng không tìm thấy.", 404));

        return Ok(new Response<List<BuyerOrder>>(orders, RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), "Ok", $"Trả về danh sách hóa đơn của khách với mã {id} thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy hóa đơn bằng id"), "Not Found", "Hoá đơn không tìm thấy.", 404));

        return Ok(new Response<BuyerOrder>(order, RequestMessage.Text("Lấy hóa đơn bằng id"), "Ok", "Trả về hóa đơn thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), "Bad Request", "Dữ liệu không hợp lệ."));

        try
        {
            OrderAddress shipAddress = new OrderAddress(request.Street, request.City, request.State, request.Country);
            OrderStatus orderStatus = new OrderStatus(request.Status, request.CompleteTime);

            var result = await _orderService.CreateOrder(request.BuyerId, request.BasketId, shipAddress, orderStatus);

            if (result == null)
                return NotFound(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), "Not Found", "Giỏ hàng của khách không tìm thấy.", 404));

            return Ok(new Response<BuyerOrder>(result, RequestMessage.Text("Tạo hóa đơn mới cho người mua"), "Created", "Hóa đơn tạo ra thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, string orderStatus)
    {
        var result = await _orderService.UpdateOrderStatus(id, orderStatus);

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Cập nhật hóa đơn"), "Not Found", "Hóa đơn của khách không tìm thấy.", 404));

        return Ok(new Response<BuyerOrder>(result, RequestMessage.Text("Cập nhật hóa đơn"), "No Content", "Cập nhật hóa đơn thành công."));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _orderService.DeleteOrder(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa hóa đơn của người mua"), "Not Found", "Hóa đơn không tìm thấy.", 404));

        return Ok(new Response<bool>(result, RequestMessage.Text("Xóa hóa đơn của người mua"), "No Content", "Xóa hóa đơn thành công."));
    }
}
