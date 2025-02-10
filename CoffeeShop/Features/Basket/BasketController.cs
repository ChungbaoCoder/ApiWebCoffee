using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Basket;

[ApiController]
[Route("api/[controller]")]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateBasketForUser(int buyerId)
    {
        var basket = await _basketService.CreateBasketForUser(buyerId);

        if (basket == null)
            return StatusCode(404, new Response<object>(RequestMessage.Text("Tạo giỏ hàng cho người dùng"), "Not Found", "Người mua không tìm thấy.", 404));

        return Ok(new Response<BuyerBasket>(basket, RequestMessage.Text("Tạo giỏ hàng cho người dùng"), "Created",  "Tạo giỏ hàng thành công.", 201));
    }

    [HttpPost("AddItem")]
    public async Task<IActionResult> AddItemToBasket([FromBody] BasketItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), "Bad Request", "Dữ liệu không hợp lệ."));

        try
        {
            var basket = await _basketService.AddItemToBasket(request.BuyerId, request.BasketId, request.CoffeeItemId, request.Quantity);

            if (basket == null)
                return StatusCode(404, new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), "Not Found", "Giỏ hàng hoặc sản phẩm không tìm thấy.", 404));

            await ClearBasket(request.BasketId);
            return Ok(new Response<BuyerBasket>(basket, RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), "No Content", "Sản phẩm thêm vào thành công." , 204));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), "Internal Server Error", $"Lỗi: {ex.Message}.", 500));
        }
    }

    [HttpDelete("Product/{id}")]
    public async Task<IActionResult> RemoveItemFromBasket(int basketId, int id)
    {
        var result = await _basketService.RemoveItemFromBasket(basketId, id);

        if (result == null)
            return StatusCode(404, new Response<object>(RequestMessage.Text("Xóa sản phẩm khỏi giỏ hàng"), "Not Found", "Giỏ hàng hoặc sản phẩm không tìm thấy.", 404));

        return Ok(new Response<bool>(RequestMessage.Text("Xóa sản phẩm khỏi giỏ hàng bằng id sản phẩm"), "No Content", "Xóa giỏ hàng thành công.", 204));
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteBasket(int id)
    {
        var result = await _basketService.DeleteBasket(id);

        if (result == false)
            return StatusCode(404, new Response<object>(RequestMessage.Text("Xóa giỏ hàng bằng id"), "Not Found", "Giỏ hàng không tìm thấy.", 404));

        return Ok(new Response<bool>(result, RequestMessage.Text("Xóa giỏ hàng bằng id"), "No Content", "Xóa giỏ hàng thành công.", 204));
    }

    private async Task ClearBasket(int id)
    {
        await _basketService.ClearBasket(id);
    }
}
