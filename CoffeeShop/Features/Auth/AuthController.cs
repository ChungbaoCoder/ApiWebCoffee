using System.Net;
using CoffeeShop.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Features.Auth;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("user/register")]
    public async Task<ActionResult<AuthRequest>> Register([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Đăng kí người dùng mới"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var result = await _authService.RegisterCustomer(request.Name, request.Email, request.PhoneNum, request.Password);

        if (result == false)
        {
            return BadRequest(new Response<object>(RequestMessage.Text("Đăng kí người dùng mới"), HttpStatusCode.BadRequest, $"Email {request.Email} của người dùng này đã có."));
        }

        return Created(Request.Path, new Response<AuthRequest>(RequestMessage.Text("Đăng kí người dùng mới"), HttpStatusCode.Created, "Người dùng đã được tạo.", request));
    }

    [HttpPost("user/login")]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<object>(RequestMessage.Text("Đăng nhập vào tài khoản"), HttpStatusCode.BadRequest, "Dữ liệu không hợp lệ."));

        var result = await _authService.LoginUser(request.Email, request.Password);

        if (result == null)
            return Unauthorized(new Response<string>(RequestMessage.Text("Đăng nhập vào tài khoản"), HttpStatusCode.Unauthorized, $"Email {request.Email} của người dùng không có trong dữ liệu.", request.Email));

        return Ok(new Response<TokenResponse>(RequestMessage.Text("Đăng nhập vào tài khoản và trả về token"), HttpStatusCode.OK, "Tạo token refresh cho người dùng", result));
    }
}
