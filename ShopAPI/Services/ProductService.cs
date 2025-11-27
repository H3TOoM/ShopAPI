using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    /// <summary>
    /// Service for managing product operations including CRUD and search/filter functionality
    /// </summary>
    public class ProductService : IProductService
    {
        #region Dependencies

        private readonly IMainRepoistory<Product> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ProductService
        /// </summary>
        /// <param name="mainRepository">Repository for product data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public ProductService(IMainRepoistory<Product> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="dto">Product creation data transfer object</param>
        /// <returns>Created product view DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<ProductViewDto> CreateProductAsync(ProductCreateDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            // Map DTO to entity and save to database
            var product = _mapper.Map<Product>(dto);
            await _mainRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductViewDto>(product);
        }

        /// <summary>
        /// Deletes a product by ID
        /// </summary>
        /// <param name="id">Product ID to delete</param>
        /// <returns>True if deletion was successful, false if product not found</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid</exception>
        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var product = await _mainRepository.GetByIdAsync(id);
            if (product.IsNotFound())
                return false;

            await _mainRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of all products</returns>
        public async Task<IEnumerable<ProductViewDto>> GetAllProductsAsync()
        {
            var products = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductViewDto>>(products);
        }

        /// <summary>
        /// Gets a product by ID
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns>Product view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or product not found</exception>
        public async Task<ProductViewDto> GetProductByIdAsync(int productId)
        {
            if (productId.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var product = await _mainRepository.GetByIdAsync(productId);
            if (product.IsNotFound())
                throw new ArgumentException("Product Not Found");

            return _mapper.Map<ProductViewDto>(product);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Product ID to update</param>
        /// <param name="dto">Product update data transfer object</param>
        /// <returns>Updated product view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or product not found</exception>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<ProductViewDto> UpdateProductAsync(int id, ProductUpdateDto dto)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            var product = await _mainRepository.GetByIdAsync(id);
            if (product.IsNotFound())
                throw new ArgumentException("Product Not Found");

            // Update only provided fields (null-coalescing for optional updates)
            product.Name = dto.Name ?? product.Name;
            if (dto.Price != 0)
                product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl ?? product.ImageUrl;
            if (dto.CategoryId != 0)
                product.CategoryId = dto.CategoryId;

            await _mainRepository.UpdateAsync(id, product);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProductViewDto>(product);
        }

        #endregion

        #region Query Operations

        /// <summary>
        /// Filters products by price range
        /// </summary>
        /// <param name="minPrice">Minimum price</param>
        /// <param name="maxPrice">Maximum price</param>
        /// <returns>List of products within the price range</returns>
        public async Task<IEnumerable<ProductViewDto>> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = await _mainRepository.GetAllAsync();
            var filteredProducts = products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(filteredProducts);
        }

        /// <summary>
        /// Gets products by category ID
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>List of products in the specified category</returns>
        /// <exception cref="ArgumentException">Thrown when category ID is invalid</exception>
        public async Task<IEnumerable<ProductViewDto>> GetProductsByCategoryAsync(int categoryId)
        {
            if (categoryId.IsInValidId())
                throw new ArgumentException("Invalid Category ID!");

            var products = await _mainRepository.GetAllAsync();
            var filteredProducts = products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(filteredProducts);
        }

        /// <summary>
        /// Searches products by name (case-insensitive)
        /// </summary>
        /// <param name="searchTerm">Search term to match against product names</param>
        /// <returns>List of products matching the search term</returns>
        /// <exception cref="ArgumentException">Thrown when search term is empty</exception>
        public async Task<IEnumerable<ProductViewDto>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty");

            var products = await _mainRepository.GetAllAsync();
            var searchedProducts = products
                .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(searchedProducts);
        }

        /// <summary>
        /// Sorts products by price in descending order
        /// </summary>
        /// <param name="price">Price parameter (currently unused, kept for interface compatibility)</param>
        /// <returns>List of products sorted by price (highest first)</returns>
        public async Task<IEnumerable<ProductViewDto>> SortByPrice(decimal price)
        {
            var products = await _mainRepository.GetAllAsync();
            var sortedProducts = products
                .OrderByDescending(p => p.Price)
                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(sortedProducts);
        }

        #endregion
    }
}
