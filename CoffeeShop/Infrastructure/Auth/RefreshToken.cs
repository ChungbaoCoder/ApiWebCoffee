using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CoffeeShop.Infrastructure.Auth;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string JwtId { get; set; } = string.Empty;
    public bool IsRevoked { get; set; }
    public DateTime DateAdded { get; set; }
    public DateTime DateExpired { get; set; }

    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]
    public ApplicationUser User { get; set; }
}
