using System.Security.Claims;
using System.Security.Cryptography;
using CoffeeShop.Database;
using CoffeeShop.Features.Basket;
using CoffeeShop.Features.Buyer;
using CoffeeShop.Features.Coffee;
using CoffeeShop.Features.Order;
using CoffeeShop.Infrastructure.Auth;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<UserDbContext>()
//    .AddDefaultTokenProviders();

var rsaKey = RSA.Create();
rsaKey.ImportRSAPrivateKey(File.ReadAllBytes("key"), out _);

builder.Services.AddAuthentication("JswToken")
.AddJwtBearer("JswToken", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false, 
        ValidateIssuer = false,
    };

    o.Events = new JwtBearerEvents()
    {
        OnMessageReceived = (ctx) =>
        {
            if (ctx.Request.Query.ContainsKey("t"))
            {
                ctx.Token = ctx.Request.Query["t"];
            }
            return Task.CompletedTask;
        }
    };

    o.Configuration = new OpenIdConnectConfiguration()
    {
        SigningKeys = { new RsaSecurityKey(rsaKey) }
    };

    o.MapInboundClaims = false;
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

app.MapGet("/", (HttpContext ctx) => Results.Content($"{ctx.User.FindFirst("sub")?.Value ?? "empty key <br> <a href='/GetKey'>Click here to get your key</a>"} ", "text/html"));

app.MapGet("/GetKey", () =>
{
    var handler = new JsonWebTokenHandler();
    var key = new RsaSecurityKey(rsaKey);
    var token = handler.CreateToken(new SecurityTokenDescriptor
    {
        Issuer = "https://localhost:5000",
        Subject = new ClaimsIdentity(new[]
        {
            new Claim("sub", Guid.NewGuid().ToString()),
            new Claim("name", "registerUser")
        }),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
    });
    return token;
});

app.MapGet("/GetJWT-public", () =>
{
    var publicKey = RSA.Create();
    publicKey.ImportRSAPublicKey(rsaKey.ExportRSAPublicKey(), out _);
    var key = new RsaSecurityKey(publicKey);
    return JsonWebKeyConverter.ConvertFromRSASecurityKey(key);
});


app.MapGet("/GetJWT-private", () =>
{
    var key = new RsaSecurityKey(rsaKey);
    return JsonWebKeyConverter.ConvertFromRSASecurityKey(key);
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
