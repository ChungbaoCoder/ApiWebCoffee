using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Order;

public class OrderRequest
{
    [Required(ErrorMessage = "Cần id người mua")]
    public int BuyerId { get; set; }
    [Required(ErrorMessage = "Cần id giỏ hàng của người mua")]
    public int BasketId { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    [Required(ErrorMessage = "Cần trạng thái hóa đơn")]
    public string Status { get; set; }
    [Required(ErrorMessage = "Cần thời gian giao hóa đơn")]
    public DateTime CompleteTime { get; set; }
}
