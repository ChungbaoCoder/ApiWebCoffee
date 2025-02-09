namespace CoffeeShop.Features.Auth;

public class TokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateExpired { get; set; }
}
