﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoffeeShop.Database;
using CoffeeShop.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly CoffeeDbContext _context;
    private readonly IConfiguration _configuration;
    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CoffeeDbContext cotext, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = cotext;
        _configuration = configuration;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Đăng kí người dùng mới"), "Bad Request", "Dữ liệu không hợp lệ."));

        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            return BadRequest(new Response<AuthRequest>(request, RequestMessage.Text("Đăng kí người dùng mới"), "Bad Request", $"Email {request.Email} của người dùng này đã có."));
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return StatusCode(500, new Response<AuthRequest>(request, RequestMessage.Text("Đăng kí người dùng mới"), "Bad Request", "Người dùng không thể tạo được.", 500));
        }

        await _userManager.AddToRoleAsync(user, "User");
        return Ok(new Response<AuthRequest>(request, RequestMessage.Text("Đăng kí người dùng mới"), "Ok", "Người dùng đã được tạo.", 201));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Đăng nhập vào tài khoản"), "Bad Request", "Dữ liệu không hợp lệ."));

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Unauthorized(new Response<string>(email, RequestMessage.Text("Đăng nhập vào tài khoản"), "Unauthorized", $"Email {email} của người dùng không có trong dữ liệu.", 401));

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!result.Succeeded)
            return Unauthorized(new Response<object>(RequestMessage.Text("Đăng nhập vào tài khoản"), "Unauthorized", "Mật khẩu của người dùng không có trong dữ liệu.", 401));

        var token = await GenerateJwtToken(user);
        return Ok(new Response<TokenResponse>(token, RequestMessage.Text("Đăng nhập vào tài khoản và trả về token"), "Created", "Tạo token refresh cho người dùng", 201));
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
            expires: DateTime.Now.AddMinutes(5),
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
