using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Basket;

public class BasketItemRequest
{
    [Required(ErrorMessage = "Cần id của sản phẩm")]
    [Range(1, double.MaxValue, ErrorMessage = "Mã sản phẩm không được là số âm")]
    public int ItemVariantId { get; set; }
    [Required(ErrorMessage = "Cần phải có số lượng sản phẩm")]
    [Range(1, 99, ErrorMessage = "Số lượng phải là số dương.")]
    public int Quantity { get; set; }
}
