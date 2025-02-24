using System.Net;
using CoffeeShop.Entities.GroupBasket;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Basket;

[ApiController]
[Route("api/basket")]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{basketId}")]
    public async Task<ActionResult<BuyerBasket>> GetBasketById(int basketId)
    {
        try
        {
            var basket = await _basketService.ViewBasket(basketId);
            return Ok(new Response<BuyerBasket>(RequestMessage.Text("Tìm giỏ hàng bằng id"), HttpStatusCode.OK, "Tìm giỏ hàng thành công.", basket));
        }
        catch (Exception ex)
        {
            return NotFound(new Response<object>(RequestMessage.Text("Tìm giỏ hàng bằng id"), HttpStatusCode.NotFound, ex.Message));
        }    
    }

    [HttpPost]
    public async Task<ActionResult<BuyerBasket>> CreateBasketForUser([FromBody] int buyerId)
    {
        try
        {
            var basket = await _basketService.CreateBasketForUser(buyerId);
            return Created(Request.Path, new Response<BuyerBasket>(RequestMessage.Text("Tạo giỏ hàng cho người dùng"), HttpStatusCode.Created, "Tạo giỏ hàng thành công."));
        }
        catch (Exception ex)
        {
            return NotFound(new Response<object>(RequestMessage.Text("Tạo giỏ hàng cho người dùng"), HttpStatusCode.NotFound, ex.Message));
        }    
    }

    [HttpPost("{basketId}/items")]
    public async Task<IActionResult> AddItemToBasket(int basketId, [FromBody] BasketItemRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var basket = await _basketService.AddItemToBasket(basketId, request.ItemVariantId, request.Quantity);

            if (basket == null)
                return NotFound(new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), HttpStatusCode.NotFound, "Giỏ hàng hoặc sản phẩm không tìm thấy."));

            await ClearBasket(basketId);
            return Ok(new Response<BuyerBasket>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), HttpStatusCode.OK, "Sản phẩm thêm vào thành công.", basket));

        }
        catch (Exception ex)
        {
            return BadRequest(new Response<object>(RequestMessage.Text("Thêm sản phẩm vào giỏ hàng"), HttpStatusCode.BadRequest, ex.Message));
        }
    }

    [HttpDelete("{basketId}/product/{id}")]
    public async Task<IActionResult> RemoveItemFromBasket(int basketId, int id)
    {
        var result = await _basketService.RemoveItemFromBasket(basketId, id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa sản phẩm khỏi giỏ hàng"), HttpStatusCode.NotFound, "Giỏ hàng hoặc sản phẩm không tìm thấy."));

        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAllItems(int id)
    {
        var result = await _basketService.DeleteAllItems(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa giỏ hàng bằng id"), HttpStatusCode.NotFound, "Giỏ hàng không tìm thấy."));

        return NoContent();
    }

    private async Task ClearBasket(int id)
    {
        await _basketService.ClearBasket(id);
    }
}
