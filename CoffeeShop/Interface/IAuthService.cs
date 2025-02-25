using CoffeeShop.Features.Auth;

namespace CoffeeShop.Interface
{
    public interface IAuthService
    {
        Task<bool> Register(string userName, string email, string phoneNum, string password);
        Task<TokenResponse> Login(string email, string password);
        Task<bool> RegisterCustomer(string userName, string email, string phoneNum, string password);
        Task<TokenResponse> LoginCustomer(string email, string password);
    }
}