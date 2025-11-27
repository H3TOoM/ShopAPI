using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    /// <summary>
    /// Service for managing shopping cart operations
    /// </summary>
    public class CartService : ICartService
    {
        #region Dependencies

        private readonly IMainRepoistory<Cart> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the CartService
        /// </summary>
        /// <param name="mainRepository">Repository for cart data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public CartService(IMainRepoistory<Cart> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Cart Operations

        /// <summary>
        /// Creates a new cart for a user
        /// </summary>
        /// <param name="userId">User ID to create cart for</param>
        /// <returns>Cart item view DTO (first item if exists, empty otherwise)</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is invalid</exception>
        public async Task<CartItemViewDto> CreateCart(int userId)
        {
            if (userId.IsInValidId())
                throw new ArgumentException("Invalid User ID!");

            // Create new cart with empty items list
            var cart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>(),
            };

            await _mainRepository.CreateAsync(cart);
            await _unitOfWork.SaveChangesAsync();

            var cartView = _mapper.Map<CartViewDto>(cart);
            return cartView.Items.FirstOrDefault() ?? new CartItemViewDto();
        }

        /// <summary>
        /// Gets a cart by user ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Cart item view DTO (first item if exists, empty otherwise)</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is invalid or cart not found</exception>
        public async Task<CartItemViewDto> GetCartByUserIdAsync(int userId)
        {
            if (userId.IsInValidId())
                throw new ArgumentException("Invalid User ID!");

            // Find cart by user ID
            var carts = await _mainRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart.IsNotFound())
                throw new ArgumentException("Cart Not Found");

            var cartView = _mapper.Map<CartViewDto>(cart);
            return cartView.Items.FirstOrDefault() ?? new CartItemViewDto();
        }

        /// <summary>
        /// Updates a cart item for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="cartItemDto">Cart item update data transfer object</param>
        /// <returns>Updated cart item view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is invalid or cart not found</exception>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<CartItemViewDto> UpdateCart(int userId, CartItemUpdateDto cartItemDto)
        {
            if (userId.IsInValidId())
                throw new ArgumentException("Invalid User ID!");

            if (cartItemDto.IsNullEntity())
                throw new ArgumentNullException(nameof(cartItemDto));

            // Find cart by user ID
            var carts = await _mainRepository.GetAllAsync();
            var userCart = carts.FirstOrDefault(c => c.UserId == userId);

            if (userCart.IsNotFound())
                throw new ArgumentException("Cart Not Found");

            // Update the first cart item if exists
            var cartItems = userCart.CartItems?.ToList() ?? new List<CartItem>();
            if (cartItems.Any())
            {
                var firstItem = cartItems.First();
                firstItem.Quantity = cartItemDto.Quantity;
                firstItem.ProductId = cartItemDto.ProductId;
            }

            await _mainRepository.UpdateAsync(userCart.Id, userCart);
            await _unitOfWork.SaveChangesAsync();

            var updatedCartView = _mapper.Map<CartViewDto>(userCart);
            return updatedCartView.Items.FirstOrDefault() ?? new CartItemViewDto();
        }

        /// <summary>
        /// Clears all items from a user's cart
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>True if cart was cleared, false if cart not found</returns>
        /// <exception cref="ArgumentException">Thrown when user ID is invalid</exception>
        public async Task<bool> ClearCart(int userId)
        {
            if (userId.IsInValidId())
                throw new ArgumentException("Invalid User ID!");

            // Find cart by user ID
            var carts = await _mainRepository.GetAllAsync();
            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart.IsNotFound())
                return false;

            // Clear cart items by setting to empty list
            cart.CartItems = new List<CartItem>();
            await _mainRepository.UpdateAsync(cart.Id, cart);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}

