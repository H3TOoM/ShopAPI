using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface ICartService
    {
        Task<CartItemViewDto> GetCartByUserIdAsync(int userId);
        Task<CartItemViewDto> CreateCart(int userId);

        Task<CartItemViewDto> UpdateCart(int userId, CartItemUpdateDto cart);

        Task<bool> ClearCart(int userId);
    }
}
