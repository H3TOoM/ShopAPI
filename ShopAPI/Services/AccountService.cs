using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Helpers;
using ShopAPI.Models;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services.Base;

namespace ShopAPI.Services
{
    /// <summary>
    /// Service for managing user account operations including registration and authentication
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Dependencies

        private readonly IMainRepoistory<User> _mainRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AccountService
        /// </summary>
        /// <param name="mainRepository">Repository for user data access</param>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        public AccountService(IMainRepoistory<User> mainRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Authentication Operations

        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="dto">User registration data transfer object</param>
        /// <returns>Registered user view DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        /// <exception cref="ArgumentException">Thrown when user with email already exists</exception>
        /// <remarks>Password should be hashed in production environment</remarks>
        public async Task<UserViewDto> RegisterAsync(UserCreateDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            // Check if user already exists by email
            var users = await _mainRepository.GetAllAsync();
            var existingUser = users.FirstOrDefault(u => u.Email == dto.Email);
            if (existingUser != null)
                throw new ArgumentException("User with this email already exists");

            // Map DTO to entity and set password hash
            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _mainRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserViewDto>(user);
        }

        /// <summary>
        /// Authenticates a user and logs them in
        /// </summary>
        /// <param name="dto">User login data transfer object</param>
        /// <returns>Authenticated user view DTO</returns>
        /// <exception cref="ArgumentNullException">Thrown when DTO is null</exception>
        /// <exception cref="ArgumentException">Thrown when email or password is invalid</exception>
        /// <remarks>Password comparison should use hashing in production environment</remarks>
        public async Task<UserViewDto> LoginAsync(UserLoginDto dto)
        {
            if (dto.IsNullEntity())
                throw new ArgumentNullException(nameof(dto));

            // Find user by email and password
            var users = await _mainRepository.GetAllAsync();
            var user = users.FirstOrDefault(u =>
                u.Email == dto.Email &&
                u.PasswordHash == BCrypt.Net.BCrypt.HashPassword(dto.Password));

            if (user.IsNotFound())
                throw new ArgumentException("Invalid email or password");

            return _mapper.Map<UserViewDto>(user);
        }

        #endregion
    }
}


