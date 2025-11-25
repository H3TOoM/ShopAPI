using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressViewDto>> GetAllAddressesAsync();

        Task<IEnumerable<AddressViewDto>> GetAddressesByUserIdAsync(int userId);
        Task<AddressViewDto> GetAddressByIdAsync( int id);

        Task<AddressViewDto> CreateAddressAsync(int userId, AddressCreateDto dto);
        Task<AddressViewDto> UpdateAddressAsync(int userId, AddressUpdateDto dto);
        
        Task<bool> DeleteAddressAsync(int id);

    }
}
