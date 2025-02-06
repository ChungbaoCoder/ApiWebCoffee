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
    public async Task<IActionResult> CreateBasketForUser([FromBody] int buyerId)
    {
        var basket = await _basketService.CreateBasketForUser(buyerId);

        if (basket == null)
            return StatusCode(404, new Response<object>("Không thể tìm thấy giỏ hàng.", 404));

        return Ok(new Response<BuyerBasket>(basket, "Tạo giỏ hàng thành công."));
    }

    [HttpPost("AddItem")]
    public async Task<IActionResult> AddItemToBasket([FromBody] BasketItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>("Dữ liệu không hợp lệ."));

        try
        {
            var basket = await _basketService.AddItemToBasket(request.BuyerId, request.CoffeeItemId, request.Price, request.Quantity);

            if (basket == null)
                return StatusCode(500, new Response<object>("Lỗi thêm hàng vào giỏ hàng.", 500));

            return Ok(new Response<BuyerBasket>(basket, "Sản phẩm được thêm vào thành công."));
        }
        catch (Exception)
        {
            return StatusCode(500, new Response<object>("Lỗi thêm sản phẩm vào giỏ hàng.", 500));
        }
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteBasket(int id)
    {
        var result = await _basketService.DeleteBasket(id);

        if (result == false)
            return NotFound(new Response<object>("Không thể tìm thấy giỏ hàng.", 404));

        return Ok(new Response<bool>(result, "Giỏ hàng đã xóa thành công."));
    }
}
