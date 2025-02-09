using System.Security.Claims;
using System.Text;
using CoffeeShop.Database;
using CoffeeShop.Features.Basket;
using CoffeeShop.Features.Buyer;
using CoffeeShop.Features.Coffee;
using CoffeeShop.Features.Order;
using CoffeeShop.Infrastructure.Auth;
using CoffeeShop.Infrastructure.SeedData;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICoffeeItemService, CoffeeService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<CoffeeDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<CoffeeDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = "JswToken";
    o.DefaultChallengeScheme = "JswToken";
    o.DefaultScheme = "JswToken";
})
.AddJwtBearer("JswToken", o =>
{
    o.SaveToken = true;
    o.RequireHttpsMetadata = false;

    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
    };
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", p =>
    {
        p.RequireClaim(ClaimTypes.Role, "Admin");
    });

    o.AddPolicy("User", p =>
    {
        p.RequireClaim(ClaimTypes.Role, "User");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await SeedData(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

async Task SeedData(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SeedRole.SeedAdminUser(serviceProvider, userManager, roleManager);
    }
}
