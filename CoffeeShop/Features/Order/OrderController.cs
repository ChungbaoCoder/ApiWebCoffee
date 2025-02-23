﻿using System.Net;
using CoffeeShop.Entities.GroupOrder;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Order;

[ApiController]
[Route("api/order")]
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

        if (orders == null || orders.Count == 0)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), HttpStatusCode.NotFound, "Danh sách hóa đơn của người dùng không tìm thấy."));

        return Ok(new Response<List<BuyerOrder>>(RequestMessage.Text("Lấy danh sách hóa đơn của người dùng bằng id"), HttpStatusCode.OK, $"Trả về danh sách hóa đơn của khách với mã {id} thành công.", orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuyerOrder>> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);

        if (order == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy hóa đơn bằng id"), HttpStatusCode.NotFound, "Hoá đơn không tìm thấy."));

        return Ok(new Response<BuyerOrder>(RequestMessage.Text("Lấy hóa đơn bằng id"), HttpStatusCode.OK, "Trả về hóa đơn thành công.", order));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var listOrderItems = request.OrderItems.Select(oi => new OrderItem(oi.ItemVariantId, oi.Price, oi.Quantity)).ToList();

            var result = await _orderService.CreateOrder(request.BuyerId, listOrderItems);

            if (result == null)
                return NotFound(new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.NotFound, "Khách hàng không tìm thấy."));

            return Created(Request.Path, new Response<BuyerOrder>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.Created, "Hóa đơn tạo ra thành công."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Tạo hóa đơn mới cho người mua"), HttpStatusCode.InternalServerError, $"Lỗi: {ex.Message}."));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus orderStatus, [FromBody] PaymentStatus paymentStatus)
    {
        var result = await _orderService.UpdateOrderStatus(id, orderStatus, paymentStatus);

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
