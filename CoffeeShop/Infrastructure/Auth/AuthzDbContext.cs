using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infrastructure.Auth
{
    public class AuthzDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthzDbContext(DbContextOptions<AuthzDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
