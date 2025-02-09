using CoffeeShop.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Infrastructure.SeedData;

public class SeedAdmin
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var roleExist = await roleManager.RoleExistsAsync("Admin");
        if (!roleExist)
        {
            var role = new IdentityRole("Admin");
            await roleManager.CreateAsync(role);
        }

        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admintest",
                Email = "admin@gmail.com"
            };
            await userManager.CreateAsync(adminUser, "AdminPass@word123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
