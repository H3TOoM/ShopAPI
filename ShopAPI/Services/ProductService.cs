using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMainRepoistory<Product> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IMainRepoistory<Product> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductViewDto> CreateProductAsync(ProductCreateDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ImageUrl = dto.ImageUrl,
            };

            await _mainRepository.CreateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductViewDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id.IsInValidId())
                throw new ArgumentOutOfRangeException(nameof(id));

            var product = await _mainRepository.GetByIdAsync(id);
            if (product.IsNotFound())
                throw new KeyNotFoundException(nameof(id));

            await _mainRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductViewDto>> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = await _mainRepository.GetAllAsync();
            var filteredProducts = products.Where(p =>
                                   p.Price <= maxPrice && p.Price >= minPrice)
                                    .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(filteredProducts);
        }

        public async Task<IEnumerable<ProductViewDto>> GetAllProductsAsync()
        {
            var products = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductViewDto>>(products);
        }

        public async Task<ProductViewDto> GetProductByIdAsync(int productId)
        {
            if (productId.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var product = await _mainRepository.GetByIdAsync(productId);
            if (product.IsNotFound())
                throw new ArgumentException("Product Not Found");

            return _mapper.Map<ProductViewDto>(product);
        }

        public async Task<IEnumerable<ProductViewDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _mainRepository.GetAllAsync();
            var filteredProducts = products.Where(p => p.CategoryId == categoryId)
                                                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(filteredProducts);
        }

        public async Task<IEnumerable<ProductViewDto>> SearchProductsAsync(string searchTerm)
        {
            var products = await _mainRepository.GetAllAsync();
            var searchedProducts = products.Where(p => p.Name.Contains(searchTerm))
                                .ToList();

            return _mapper.Map<IEnumerable<ProductViewDto>>(searchedProducts);
        }

        public async Task<IEnumerable<ProductViewDto>> SortByPrice(decimal price)
        {
            var products = await _mainRepository.GetAllAsync();
            var sortedProducts = products.OrderByDescending(p => p.Price);
            return _mapper.Map<IEnumerable<ProductViewDto>>(sortedProducts);
        }

        public async Task<ProductViewDto> UpdateProductAsync(int id, ProductUpdateDto dto)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var product = await _mainRepository.GetByIdAsync(id);
            if (product.IsNotFound())
                throw new ArgumentException("Product Not Found!");

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
    }
}
