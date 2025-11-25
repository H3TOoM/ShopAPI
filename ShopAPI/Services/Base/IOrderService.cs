using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewDto>> GetAllOrdersAsync();
        Task<OrderViewDto> GetOrderByIdAsync(int orderId);
        Task<OrderViewDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderViewDto> UpdateOrderAsync(int id, OrderUpdateDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
