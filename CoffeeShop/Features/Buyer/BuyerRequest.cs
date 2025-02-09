using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Buyer;

public class BuyerRequest
{
    [Required(ErrorMessage = "Cần tên của khách hàng")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email cần phải có")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    public string Email { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}
