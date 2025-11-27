using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    /// <summary>
    /// Service for managing category operations including CRUD functionality
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Dependencies

        private readonly IMainRepoistory<Category> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CategoryService
        /// </summary>
        /// <param name="mainRepository">Repository for category data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public CategoryService(IMainRepoistory<Category> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new category
        /// </summary>
        /// <param name="dto">Category creation data transfer object</param>
        /// <returns>Created category view DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<CategoryViewDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            // Map DTO to entity and save to database
            var category = _mapper.Map<Category>(dto);
            await _mainRepository.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryViewDto>(category);
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>List of all categories</returns>
        public async Task<IEnumerable<CategoryViewDto>> GetAllCategoriesAsync()
        {
            var categories = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryViewDto>>(categories);
        }

        /// <summary>
        /// Gets a category by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Category view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or category not found</exception>
        public async Task<CategoryViewDto> GetCategoryByIdAsync(int id)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var category = await _mainRepository.GetByIdAsync(id);
            if (category.IsNotFound())
                throw new ArgumentException("Category Not Found");

            return _mapper.Map<CategoryViewDto>(category);
        }

        /// <summary>
        /// Updates an existing category
        /// </summary>
        /// <param name="id">Category ID to update</param>
        /// <param name="dto">Category update data transfer object</param>
        /// <returns>Updated category view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or category not found</exception>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<CategoryViewDto> UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var category = await _mainRepository.GetByIdAsync(id);
            if (category.IsNotFound())
                throw new ArgumentException("Category Not Found");

            // Update category name if provided
            category.Name = dto.Name ?? category.Name;
            await _mainRepository.UpdateAsync(id, category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryViewDto>(category);
        }

        /// <summary>
        /// Deletes a category by ID
        /// </summary>
        /// <param name="id">Category ID to delete</param>
        /// <returns>True if deletion was successful, false if category not found</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid</exception>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var category = await _mainRepository.GetByIdAsync(id);
            if (category.IsNotFound())
                return false;

            await _mainRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}

