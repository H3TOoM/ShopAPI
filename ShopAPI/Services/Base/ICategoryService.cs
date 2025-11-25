using ShopAPI.DTOs;

namespace ShopAPI.Services.Base
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync();
        Task<CategoryViewDto> GetCategoryByIdAsync(int id);
        Task<CategoryViewDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task<CategoryViewDto> UpdateCategoryAsync(int id, CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
