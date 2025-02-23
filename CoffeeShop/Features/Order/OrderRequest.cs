using System.ComponentModel.DataAnnotations;
using CoffeeShop.Entities.GroupOrder;

namespace CoffeeShop.Features.Order;

public class OrderRequest
{
    [Required(ErrorMessage = "Cần id người mua")]
    [Range(1, double.MaxValue, ErrorMessage = "Mã người mua không được là số âm")]
    public int BuyerId { get; set; }
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}
