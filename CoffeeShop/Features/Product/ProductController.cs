﻿using System.Net;
using CoffeeShop.Entities.GroupItem;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Product;

[ApiController]
[Route("api/product")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductItem>>> GetListProductItems([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest(new Response<object>(RequestMessage.Text("Lấy danh sách sản phẩm"), HttpStatusCode.BadRequest, "Số sản phẩm và số trang cần hiển thị phải là số dương."));

        var productItems = await _productService.ListItem(page, pageSize);

        if (!productItems.Any())
            return NotFound(new Response<object>(RequestMessage.Text("Lấy danh sách sản phẩm"), HttpStatusCode.NotFound, "Danh sách sản phẩm không tìm thấy."));

        return Ok(new Response<IEnumerable<ProductItem>>(RequestMessage.Text("Lấy danh sách sản phẩm"), HttpStatusCode.OK, "Trả về danh sách sản phẩm thành công.", productItems));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductItem>> GetProductItemById(int id)
    {
        var productItem = await _productService.GetById(id);

        if (productItem == null)
            return NotFound(new Response<object>(RequestMessage.Text("Lấy sản phẩm bằng id"), HttpStatusCode.NotFound, "Sản phẩm không tìm thấy."));

        return Ok(new Response<ProductItem>(RequestMessage.Text("Lấy sản phẩm bằng id"), HttpStatusCode.OK, "Trả về sản phẩm thành công.", productItem));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductItem([FromBody] CreateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Tạo sản phẩm"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var productItem = new ProductItem(
                request.Name,
                request.Description,
                request.Category,
                request.PictureUri ?? ""
            );

        var listItemVariants = request.ItemVariant.Select(iv => new ItemVariant(iv.Size, iv.StockQuantity, iv.Price, iv.Status)).ToList();

        var result = await _productService.CreateItem(productItem, listItemVariants);
        return Created(Request.Path, new Response<ProductItem>(RequestMessage.Text("Tạo sản phẩm"), HttpStatusCode.Created, "Sản phẩm tạo ra thành công.", result));
    }

    [HttpPost("{productId}/variant")]
    public async Task<ActionResult<ProductItem>> Addvariant(int productId, [FromBody] List<ItemVariantRequest> requests)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Thêm loại sản phẩm"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var variants = requests.Select(iv => new ItemVariant(iv.Size, iv.StockQuantity, iv.Price, iv.Status)).ToList();

        var result = await _productService.AddItemVariant(productId, variants);

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Thêm loại sản phẩm"), HttpStatusCode.NotFound, "Thêm loại sản phẩm không thành công."));

        return Created(Request.Path, new Response<ProductItem>(RequestMessage.Text("Thêm loại sản phẩm"), HttpStatusCode.Created, "Thêm loại sản phẩm thành công.", result));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductItem(int id, [FromBody] UpdateProductRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        try
        {
            var productItem = new ProductItem(
                request.Name,
                request.Description,
                request.Category,
                request.PictureUri ?? ""
            );

            var result = await _productService.UpdateItem(id, productItem);

            if (result == null)
                return NotFound(new Response<object>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.NotFound, "Sản phẩm không tìm thấy."));

            return Ok(new Response<ProductItem>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.NoContent, "Cập nhật sản phẩm thành công.", result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response<object>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.InternalServerError, $"Lỗi: {ex.Message}."));
        }
    }

    [HttpPut("{productItemId}/variant/{itemVariantId}")]
    public async Task<ActionResult<ProductItem>> UpdateVariant(int productItemId, int itemVariantId, ItemVariantRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Cập nhật loại sản phẩm"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var result = await _productService.UpdateItemVariants(productItemId, itemVariantId, new ItemVariant(request.Size, request.StockQuantity, request.Price, request.Status));

        if (result == null)
            return NotFound(new Response<object>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.NotFound, "Sản phẩm không tìm thấy."));

        return Ok(new Response<ProductItem>(RequestMessage.Text("Cập nhật sản phẩm"), HttpStatusCode.NoContent, "Cập nhật sản phẩm thành công.", result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductItem(int id)
    {
        var result = await _productService.DeleteItem(id);

        if (result == false)
            return NotFound(new Response<object>(RequestMessage.Text("Xóa sản phẩm bằng id"), HttpStatusCode.NotFound, "Sản phẩm không tìm thấy."));

        return NoContent();
    }
}
