using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemViewDto>> GetAllCartItemsAsync(int cartId);
        Task<CartItemViewDto> GetCartItemByIdAsync(int id);

        Task<CartItemViewDto> CreateCartItemAsync(int cartId, CartItemCreateDto dto);

        Task<CartItemViewDto> UpdateCartItemAsync(int id, CartItemUpdateDto dto);

        Task<bool> DeleteCartItemAsync(int id);
    }
}
