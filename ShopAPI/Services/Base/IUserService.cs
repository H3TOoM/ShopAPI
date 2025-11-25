using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewDto>> GetAllUsersAsync();
        Task<UserViewDto> GetUserByIdAsync(int id);
        Task<UserViewDto> CreateUserAsync(UserCreateDto dto);
        Task<UserViewDto> UpdateUserAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
