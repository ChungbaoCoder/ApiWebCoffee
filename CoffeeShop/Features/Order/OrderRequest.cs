using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Order;

public class OrderRequest
{
    [Required]
    public int BuyerId { get; set; }
    [Required]
    public int BasketId { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    [Required]
    public string Status { get; set; }
    [Required]
    public DateTime CompleteTime { get; set; }
}
