using CoffeeShop.Features.Auth;

namespace CoffeeShop.Interface
{
    public interface IAuthService
    {
        Task<TokenResponse> RegisterCustomer(string userName, string email, string phoneNum, string password);
        Task<TokenResponse> LoginUser(string email, string password);
    }
}