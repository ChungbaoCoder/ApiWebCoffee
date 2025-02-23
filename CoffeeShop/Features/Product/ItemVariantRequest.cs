using System.ComponentModel.DataAnnotations;
using CoffeeShop.Entities.GroupItem;

namespace CoffeeShop.Features.Product;

public class ItemVariantRequest
{
    public string Size { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cần phải có số lượng sản phẩm")]
    [Range(1, 99, ErrorMessage = "Số lượng sản phẩm phải từ 1 đến 99")]
    public int StockQuantity { get; set; }
    [Required(ErrorMessage = "Cần phải có giá sản phẩm")]
    [Range(1.00, 1000000000.00, ErrorMessage = "Giá phải lớn hơn 1 đ và nhỏ hơn 1.000.000.000 đ")]
    public decimal Price { get; set; }
    public ItemStatus Status { get; set; }
}
