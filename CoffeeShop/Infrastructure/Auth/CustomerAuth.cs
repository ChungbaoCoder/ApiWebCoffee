using CoffeeShop.Entities.GroupBuyer;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Infrastructure.Auth;

public class CustomerAuth
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public string AspNetUserId { get; set; }

    public string Password { get; set; }

    public virtual BuyerUser BuyerUser { get; set; }
}
