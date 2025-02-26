using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Buyer;

public class BuyerRequest
{
    [Required(ErrorMessage = "Cần tên của khách hàng")]
    [StringLength(100, ErrorMessage = "Tên không được dài quá 100 kí tự.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cần phải có email")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Cần phải có số điện thoại")]
    [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Số điện thoại không phù hợp")]
    public string PhoneNum { get; set; } = string.Empty;
}
