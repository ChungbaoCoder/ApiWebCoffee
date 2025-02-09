using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Features.Auth;

public class AuthRequest
{
    [Required(ErrorMessage = "Tên cần phải có")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email cần phải có")]
    [EmailAddress(ErrorMessage = "Không đúng định dạng email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Mật khẩu cần phải có")]
    public string Password { get; set; }
}
