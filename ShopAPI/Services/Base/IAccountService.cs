using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface IAccountService
    {
        Task<UserViewDto> RegisterAsync(UserCreateDto dto);
        Task<UserViewDto> LoginAsync(UserLoginDto dto);

        // To Do : Add other account-related methods like Logout, ChangePassword, etc.
    }
}
