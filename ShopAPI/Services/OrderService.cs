using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    /// <summary>
    /// Service for managing order operations including CRUD functionality
    /// </summary>
    public class OrderService : IOrderService
    {
        #region Dependencies

        private readonly IMainRepoistory<Order> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the OrderService
        /// </summary>
        /// <param name="mainRepository">Repository for order data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public OrderService(IMainRepoistory<Order> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="dto">Order creation data transfer object</param>
        /// <returns>Created order view DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<OrderViewDto> CreateOrderAsync(OrderCreateDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            // Map DTO to entity and set order date
            var order = _mapper.Map<Order>(dto);
            order.OrderDate = DateTime.UtcNow;

            await _mainRepository.CreateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderViewDto>(order);
        }

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>List of all orders</returns>
        public async Task<IEnumerable<OrderViewDto>> GetAllOrdersAsync()
        {
            var orders = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderViewDto>>(orders);
        }

        /// <summary>
        /// Gets an order by ID
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <returns>Order view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or order not found</exception>
        public async Task<OrderViewDto> GetOrderByIdAsync(int orderId)
        {
            if (orderId.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var order = await _mainRepository.GetByIdAsync(orderId);
            if (order.IsNotFound())
                throw new ArgumentException("Order Not Found");

            return _mapper.Map<OrderViewDto>(order);
        }

        /// <summary>
        /// Updates an existing order
        /// </summary>
        /// <param name="id">Order ID to update</param>
        /// <param name="dto">Order update data transfer object</param>
        /// <returns>Updated order view DTO</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid or order not found</exception>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        public async Task<OrderViewDto> UpdateOrderAsync(int id, OrderUpdateDto dto)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var order = await _mainRepository.GetByIdAsync(id);
            if (order.IsNotFound())
                throw new ArgumentException("Order Not Found");

            // Update order status and items if provided
            order.Status = dto.Status ?? order.Status;
            if (dto.Items != null)
            {
                var orderItems = _mapper.Map<IEnumerable<OrderItem>>(dto.Items);
                order.OrderItems = orderItems;
            }

            await _mainRepository.UpdateAsync(id, order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderViewDto>(order);
        }

        /// <summary>
        /// Deletes an order by ID
        /// </summary>
        /// <param name="id">Order ID to delete</param>
        /// <returns>True if deletion was successful, false if order not found</returns>
        /// <exception cref="ArgumentException">Thrown when ID is invalid</exception>
        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id.IsInValidId())
                throw new ArgumentException("Invalid ID!");

            var order = await _mainRepository.GetByIdAsync(id);
            if (order.IsNotFound())
                return false;

            await _mainRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
