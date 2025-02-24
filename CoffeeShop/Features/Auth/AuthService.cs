using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Database;
using CoffeeShop.Entities.GroupBuyer;
using CoffeeShop.Infrastructure.Auth;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Features.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly CoffeeDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CoffeeDbContext context, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _configuration = configuration;
    }

    public async Task<TokenResponse> RegisterCustomer(string userName, string email, string phoneNum, string password)
    {
        var existBuyer = await _context.Buyer.FirstOrDefaultAsync(b => b.Email == email && b.PhoneNum == phoneNum);

        if (existBuyer != null)
            return null;

        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNum
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Customer");
            var newBuyer = new BuyerUser(userName, email, phoneNum);

            await _context.Buyer.AddAsync(newBuyer);
            await _context.SaveChangesAsync();

            var customer = new CustomerAuth
            {
                BuyerId = newBuyer.BuyerId,
                AspNetUserId = user.Id,
                Role = UserRoleType.Customer
            };

            await _context.CustomerAuths.AddAsync(customer);
            await _context.SaveChangesAsync();
            return await GenerateJwtToken(user);
        }
        return null;
    }

    public async Task<TokenResponse> LoginUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var login = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!login.Succeeded)
            return null;

        return await GenerateJwtToken(user);
    }

    private async Task<TokenResponse> GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            IsRevoked = false,
            UserId = user.Id,
            DateAdded = DateTime.Now,
            DateExpired = DateTime.Now.AddHours(6),
            Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        var response = new TokenResponse
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token,
            DateExpired = token.ValidTo
        };
        return response;
    }
}
