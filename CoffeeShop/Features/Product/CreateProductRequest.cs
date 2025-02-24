using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Product;

public class CreateProductRequest
{
    [Required(ErrorMessage = "Cần tên của sản phẩm")]
    [StringLength(100, ErrorMessage = "Tên không được dài quá 100 kí tự.")]
    public string Name { get; set; } = string.Empty;
    [StringLength(200, ErrorMessage = "Tên không được dài quá 200 kí tự.")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cần loại sản phẩm")]
    [StringLength(100, ErrorMessage = "Tên không được dài quá 50 kí tự.")]
    public string Category { get; set; } = string.Empty;
    public string? PictureUri { get; set; }
    public List<ItemVariantRequest> ItemVariant { get; set; } = new List<ItemVariantRequest>();
}
