using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Auth;

public class AuthRequest
{
    [Required(ErrorMessage = "Tên cần phải có")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email cần phải có")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Số điện thoại cần phải có")]
    [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Số điện thoại không phù hợp")]
    public string PhoneNum { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mật khẩu cần phải có")]
    public string Password { get; set; } = string.Empty;
}
