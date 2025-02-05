using CoffeeShop.Database;
using CoffeeShop.Features.Basket;
using CoffeeShop.Features.BuyerUser;
using CoffeeShop.Features.Coffee;
using CoffeeShop.Features.Order;
using CoffeeShop.Infrastructure.Auth;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<UserDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", o =>
    {
        o.Cookie.Name = "UserId";
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
