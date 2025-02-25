using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
    private readonly PasswordHasher<BuyerUser> _passwordHasher;
    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CoffeeDbContext context, IConfiguration configuration, PasswordHasher<BuyerUser> passwordHasher)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _configuration = configuration;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> Register(string userName, string email, string phoneNum, string password)
    {
        var existUser = await _userManager.FindByEmailAsync(email);

        if (existUser != null)
            return false;

        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            PhoneNumber = phoneNum
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Moderator");
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<TokenResponse> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        var login = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!login.Succeeded)
            return null;

        return await GenerateJwtTokenUser(user);
    }

    public async Task<bool> RegisterCustomer(string userName, string email, string phoneNum, string password)
    {
        var existBuyer = await _context.Buyer.FirstOrDefaultAsync(b => b.Email == email);

        if (existBuyer != null)
            return false;

        var newBuyer = new BuyerUser(userName, email, phoneNum);

        var result = await _context.Buyer.AddAsync(newBuyer);
        await _context.SaveChangesAsync();

        var hashed = _passwordHasher.HashPassword(newBuyer, password);

        var buyer = new CustomerAuth
        {
            BuyerId = result.Entity.BuyerId,
            AspNetUserId = Guid.NewGuid().ToString(),
            Password = hashed
        };

        await _context.CustomerAuths.AddAsync(buyer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TokenResponse> LoginCustomer(string email, string password)
    {
        var buyer = await _context.Buyer.FirstOrDefaultAsync(b => b.Email == email);

        if (buyer == null)
            return null;

        var auth = await _context.CustomerAuths.FirstOrDefaultAsync(ca => ca.BuyerId == buyer.BuyerId);

        if (auth == null)
            return null;

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(buyer, auth.Password, password);

        if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                string rehash = _passwordHasher.HashPassword(buyer, password);
                auth.Password = rehash;
                await _context.SaveChangesAsync();
            }
        }
        return await GenerateJwtTokenCustomer(auth, buyer);
    }

    private async Task<TokenResponse> GenerateJwtTokenUser(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, "Moderator"),
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

    private async Task<TokenResponse> GenerateJwtTokenCustomer(CustomerAuth user, BuyerUser buyer)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.AspNetUserId),
            new Claim(ClaimTypes.Name, buyer.Name),
            new Claim(JwtRegisteredClaimNames.Email, buyer.Email),
            new Claim(JwtRegisteredClaimNames.Sub,buyer.Email),
            new Claim(ClaimTypes.Role, "Customer"),
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
            UserId = user.AspNetUserId,
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
