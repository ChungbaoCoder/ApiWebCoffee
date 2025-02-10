using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Basket;

public class BasketItemRequest
{
    [Required(ErrorMessage = "Cần id của người mua")]
    public int BuyerId { get; set; }
    [Required(ErrorMessage = "Cần id của giỏ hàng")]
    public int BasketId { get; set; }
    [Required(ErrorMessage = "Cần id của giỏ hàng")]
    public int CoffeeItemId { get; set; }
    [Range(1, 99, ErrorMessage = "Số lượng phải là số dương.")]
    public int Quantity { get; set; }
}
