using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Basket;

public class BasketItemRequest
{
    [Required]
    public int BuyerId { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải là số dương")]
    public decimal Price { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số dương.")]
    public int Quantity { get; set; }
}
