using ShopAPI.Helpers;
using ShopAPI.Models;

namespace ShopAPI.Services.Base
{
    public interface ITokenService
    {
        TokenResult GenerateToken(User user);
    }
}



