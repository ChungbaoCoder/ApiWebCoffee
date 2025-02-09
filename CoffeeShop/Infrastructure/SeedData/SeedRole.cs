using CoffeeShop.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Infrastructure.SeedData;

public class SeedRole
{
    public static async Task SeedAdminUser(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "User", "Admin", "Customer" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var role = new IdentityRole(roleName)
                {
                    NormalizedName = roleName.ToUpper()
                };
                await roleManager.CreateAsync(role);
            }
        }

        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admintest",
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            await userManager.CreateAsync(adminUser, "AdminPass@word123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
