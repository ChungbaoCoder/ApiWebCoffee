using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Features.Coffee;
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
            return NotFound(new Response<object>("Danh sách hóa đơn không tìm thấy.", 404));

        return Ok(new Response<List<BuyerOrder>>(orders, $"Trả về danh sách hóa đơn của khách với mã {id} thành công."));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order == null)
            return NotFound(new Response<object>("Hoá đơn không tìm thấy.", 404));

        return Ok(new Response<BuyerOrder>(order, "Trả về hóa đơn thành công."));
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ."));

        try
        {
            OrderAddress shipAddress = new OrderAddress(request.Street, request.City, request.State, request.Country);
            OrderStatus orderStatus = new OrderStatus(request.Status, request.CompleteTime);

            var result = await _orderService.CreateOrder(request.BuyerId, request.BasketId, shipAddress, orderStatus);

            if (result == null)
                return StatusCode(500, new Response<object>("Xảy ra sự cố khi tạo mới hóa đơn.", 500));

            return Ok(new Response<BuyerOrder>(result, "Hóa đơn tạo ra thành công.", 201));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>($"Sự cố xảy ra: {ex.Message}", 500));
        }
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, string orderStatus)
    {
        var result = await _orderService.UpdateOrderStatus(orderId, orderStatus);

        if (result == null)
            return StatusCode(404, new Response<object>("Không thể tìm thấy hóa đơn để cập nhật trạng thái.", 404));

        return Ok(new Response<BuyerOrder>(result, "Cập nhật trạng thái hóa đơn thành công."));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var result = await _orderService.CancelOrder(id);

        if (result == false)
            return NotFound(new Response<object>("Không thể tìm thấy hóa đơn để xóa.", 404));

        return Ok(new Response<bool>(result, "Hóa đơn được xóa thành công."));
    }
}
