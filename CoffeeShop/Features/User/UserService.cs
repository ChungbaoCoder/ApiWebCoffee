using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Infrastructure.Auth;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Features.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
}
