namespace CoffeeShop.Features.Auth;

public class TokenResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; }
    public DateTime DateExpired { get; set; }
}
