using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Email cần phải có")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mật khẩu cần phải có")]
    public string Password { get; set; } = string.Empty;
}
