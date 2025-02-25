using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Basket;

public class BasketMergeRequest
{
    [Required(ErrorMessage = "Cần id của sản phẩm")]
    [Range(1, double.MaxValue, ErrorMessage = "Mã sản phẩm không được là số âm")]
    public int ItemVariantId { get; set; }
    [Required(ErrorMessage = "Cần phải có giá sản phẩm")]
    [Range(1.00, 1000000000.00, ErrorMessage = "Giá phải lớn hơn 1 đ và nhỏ hơn 1.000.000.000 đ")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Cần phải có số lượng sản phẩm")]
    [Range(1, 99, ErrorMessage = "Số lượng phải là số dương.")]
    public int Quantity { get; set; }
}
