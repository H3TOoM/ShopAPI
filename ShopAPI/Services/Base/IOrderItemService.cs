using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemViewDto>> GetAllOrderItemsAsync(int orderId);
        Task<OrderItemViewDto> GetOrderItemByIdAsync(int id);

        Task<OrderItemViewDto> CreateOrderItemAsync(int orderId, OrderItemCreateDto dto);

        Task<OrderItemViewDto> UpdateOrderItemAsync(int id, OrderItemUpdateDto dto);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
