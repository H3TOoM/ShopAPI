# ShopAPI - E-Commerce REST API

A comprehensive, production-ready REST API for an e-commerce platform built with **ASP.NET Core 10** and **SQL Server**. This project demonstrates modern software architecture, best practices, and enterprise-level design patterns.

## 🎯 Project Overview

ShopAPI is a fully-featured e-commerce backend that handles product management, user authentication, shopping carts, orders, and address management. The API is built using clean architecture principles with clear separation of concerns and follows SOLID principles.

## ✨ Key Features

### Core Functionality
- **Product Management**: Browse, search, and filter products by category
- **User Authentication**: Secure JWT-based authentication with role-based authorization
- **Shopping Cart**: Add/remove items, manage cart state
- **Order Management**: Create orders, track order items and status
- **User Profiles**: Manage user information and addresses
- **Category Management**: Organize products into categories

### Technical Features
- ✅ **JWT Authentication** - Secure token-based authentication
- ✅ **Repository Pattern** - Generic repository with Unit of Work pattern
- ✅ **Dependency Injection** - Full DI container configuration
- ✅ **AutoMapper** - Object-to-object mapping
- ✅ **Entity Framework Core** - ORM with SQL Server
- ✅ **CORS Support** - Frontend integration ready
- ✅ **Swagger/OpenAPI** - Interactive API documentation
- ✅ **Async/Await** - Non-blocking operations throughout
- ✅ **Exception Handling** - Centralized error handling

## 🏗️ Architecture

### Project Structure

```
ShopAPI/
├── Controllers/          # API endpoints
├── Services/            # Business logic layer
├── Repoistires/         # Data access layer (Repository Pattern)
├── Models/              # Entity models
├── DTOs/                # Data transfer objects
├── Data/                # DbContext and migrations
├── Helpers/             # Utilities and configurations
├── Migrations/          # EF Core migrations
└── Program.cs           # Application startup configuration
```

### Layered Architecture

```
┌─────────────────────────────────────┐
│      API Controllers                │
├─────────────────────────────────────┤
│      Services (Business Logic)      │
├─────────────────────────────────────┤
│      Repositories (Data Access)     │
├─────────────────────────────────────┤
│      Entity Framework Core          │
├─────────────────────────────────────┤
│      SQL Server Database            │
└─────────────────────────────────────┘
```

## 🛠️ Technology Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core 10.0 |
| **Language** | C# 13 |
| **Database** | SQL Server |
| **ORM** | Entity Framework Core 10.0 |
| **Authentication** | JWT Bearer Tokens |
| **Mapping** | AutoMapper 12.0.1 |
| **Password Hashing** | BCrypt.Net-Next 4.0.3 |
| **API Documentation** | Swagger/OpenAPI + Scalar |

## 📋 Prerequisites

- **.NET 10 SDK** 
- **SQL Server** (2022)
- **Visual Studio 2026** or VS Code with C# extension

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/H3TOoM/ECommerce-Web-API.git
cd ShopAPI
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SHOPDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

This will create all necessary tables and schema.

### 4. Run the Application

```bash
dotnet run
```

The API will be available at: `https://localhost:5001`

### 5. Access API Documentation

- **Swagger UI**: `https://localhost:5001/openapi/v1.json`
- **Scalar UI**: `https://localhost:5001/scalar/v1`

## 📚 API Endpoints

### Authentication
```
POST   /api/account/register          - Register new user
POST   /api/account/login             - Login and get JWT token
```

### Products
```
GET    /api/products                  - Get all products
GET    /api/products/{id}             - Get product by ID
GET    /api/products/category/{id}    - Get products by category
POST   /api/products                  - Create product (Admin)
PUT    /api/products/{id}             - Update product (Admin)
DELETE /api/products/{id}             - Delete product (Admin)
```

### Categories
```
GET    /api/categories                - Get all categories
GET    /api/categories/{id}           - Get category by ID
POST   /api/categories                - Create category (Admin)
PUT    /api/categories/{id}           - Update category (Admin)
DELETE /api/categories/{id}           - Delete category (Admin)
```

### Shopping Cart
```
GET    /api/carts/{userId}            - Get user's cart
POST   /api/cartitems                 - Add item to cart
PUT    /api/cartitems/{id}            - Update cart item quantity
DELETE /api/cartitems/{id}            - Remove item from cart
```

### Orders
```
GET    /api/orders                    - Get all orders
GET    /api/orders/{id}               - Get order by ID
POST   /api/orders                    - Create new order
PUT    /api/orders/{id}               - Update order status
DELETE /api/orders/{id}               - Cancel order
```

### Users
```
GET    /api/users/{id}                - Get user profile
PUT    /api/users/{id}                - Update user profile
GET    /api/users/{id}/addresses      - Get user addresses
```

### Addresses
```
GET    /api/addresses/{id}            - Get address by ID
POST   /api/addresses                 - Create new address
PUT    /api/addresses/{id}            - Update address
DELETE /api/addresses/{id}            - Delete address
```

## 🔐 Authentication

The API uses **JWT (JSON Web Tokens)** for authentication. 

### How to Use:

1. **Register a new user**:
```bash
POST /api/account/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePassword123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

2. **Login to get token**:
```bash
POST /api/account/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePassword123!"
}
```

Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600
}
```

3. **Use token in requests**:
```bash
GET /api/products
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## 📊 Database Schema

### Core Entities

**User**
- UserId (PK)
- Email (Unique)
- PasswordHash
- FirstName
- LastName
- CreatedAt

**Product**
- ProductId (PK)
- Name
- Description
- Price
- Stock
- CategoryId (FK)
- CreatedAt

**Category**
- CategoryId (PK)
- Name
- Description

**Order**
- OrderId (PK)
- UserId (FK)
- OrderDate
- TotalAmount
- Status
- ShippingAddressId (FK)

**OrderItem**
- OrderItemId (PK)
- OrderId (FK)
- ProductId (FK)
- Quantity
- UnitPrice

**Cart**
- CartId (PK)
- UserId (FK)
- CreatedAt

**CartItem**
- CartItemId (PK)
- CartId (FK)
- ProductId (FK)
- Quantity

**Address**
- AddressId (PK)
- UserId (FK)
- Street
- City
- State
- ZipCode
- Country

## 🎨 Design Patterns Used

### 1. **Repository Pattern**
Abstracts data access logic and provides a collection-like interface for accessing domain objects.

```csharp
public interface IMainRepoistory<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(int id, T entity);
    Task<bool> DeleteAsync(int id);
}
```

### 2. **Unit of Work Pattern**
Coordinates the work of multiple repositories and ensures data consistency.

```csharp
public interface IUnitOfWork
{
    IMainRepoistory<Product> Products { get; }
    IMainRepoistory<Order> Orders { get; }
    // ... other repositories
    Task<int> SaveChangesAsync();
}
```

### 3. **Dependency Injection**
Loose coupling through constructor injection of dependencies.

```csharp
public ProductsController(IProductService productService)
{
    _productService = productService;
}
```

### 4. **Service Layer Pattern**
Encapsulates business logic separate from controllers.

```csharp
public interface IProductService
{
    Task<IEnumerable<ProductViewDto>> GetAllProductsAsync();
    Task<ProductViewDto> GetProductByIdAsync(int id);
    Task<ProductCreateDto> CreateProductAsync(ProductCreateDto dto);
    // ...
}
```

### 5. **DTO Pattern**
Separates API contracts from internal domain models.

```csharp
public class ProductViewDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    // ...
}
```

## 🔄 Data Flow Example

### Creating an Order

```
1. Client sends POST /api/orders with order details
   ↓
2. OrdersController receives request
   ↓
3. OrderService validates and processes business logic
   ↓
4. Repository pattern handles database operations
   ↓
5. Unit of Work coordinates multiple repositories
   ↓
6. Entity Framework Core executes SQL queries
   ↓
7. Response mapped to OrderViewDto and returned
```

## 🧪 Testing the API

### Using cURL

```bash
# Get all products
curl -X GET "https://localhost:5001/api/products" \
  -H "accept: application/json"

# Create a product (requires authentication)
curl -X POST "https://localhost:5001/api/products" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Laptop",
    "description": "High-performance laptop",
    "price": 999.99,
    "stock": 10,
    "categoryId": 1
  }'
```

### Using Postman

1. Import the API endpoints
2. Set up environment variables for `base_url` and `token`
3. Use the Postman collection to test all endpoints

## 📈 Performance Considerations

- **Async/Await**: All database operations are asynchronous
- **Entity Framework Optimization**: Proper use of `.Include()` for eager loading
- **Connection Pooling**: Configured in Entity Framework Core
- **Indexing**: Database indexes on frequently queried columns

## 🔒 Security Features

- **JWT Authentication**: Secure token-based authentication
- **Password Hashing**: BCrypt for secure password storage
- **CORS Configuration**: Controlled cross-origin access
- **Input Validation**: DTO validation on all endpoints
- **Authorization**: Role-based access control ready

## 📝 Configuration

### JWT Settings (appsettings.json)

```json
{
  "JwtSettings": {
    "Issuer": "ShopAPI",
    "Audience": "ShopAPI.Client",
    "SigningKey": "Your_Very_Long_Symmetric_Key_For_ShopAPI",
    "ExpiryMinutes": 60
  }
}
```

### Database Connection

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SHOPDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

## 🚦 Error Handling

The API implements centralized exception handling with meaningful error responses:

```json
{
  "statusCode": 400,
  "message": "Invalid product data",
  "errors": [
    "Price must be greater than 0",
    "Stock cannot be negative"
  ]
}
```

## 📦 Dependencies

All dependencies are managed via NuGet:

- `AutoMapper.Extensions.Microsoft.DependencyInjection` - Object mapping
- `BCrypt.Net-Next` - Password hashing
- `Microsoft.AspNetCore.Authentication.JwtBearer` - JWT authentication
- `Microsoft.EntityFrameworkCore.SqlServer` - SQL Server provider
- `Scalar.AspNetCore` - API documentation UI

## 🔄 Development Workflow

### Creating a New Feature

1. **Create Model** in `Models/`
2. **Create DTO** in `DTOs/`
3. **Add DbSet** to `AppDbContext`
4. **Create Repository** (uses generic repository)
5. **Create Service** in `Services/`
6. **Create Controller** in `Controllers/`
7. **Add Mapping** in `MappingProfile.cs`
8. **Register in DI** in `Program.cs`

### Database Migrations

```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Revert last migration
dotnet ef database update PreviousMigrationName
```

## 📚 Code Examples

### Creating a Product Service

```csharp
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductViewDto>>(products);
    }

    public async Task<ProductViewDto> CreateProductAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _unitOfWork.Products.CreateAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductViewDto>(product);
    }
}
```

### Creating a Controller

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewDto>>> GetAllAsync()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return HandleException<IEnumerable<ProductViewDto>>(ex);
        }
    }
}
```

## 🎓 Learning Outcomes

This project demonstrates:

- ✅ Clean Architecture principles
- ✅ SOLID principles (Single Responsibility, Open/Closed, Liskov, Interface Segregation, Dependency Inversion)
- ✅ Repository and Unit of Work patterns
- ✅ Dependency Injection and IoC containers
- ✅ Async/await programming
- ✅ Entity Framework Core best practices
- ✅ JWT authentication and authorization
- ✅ RESTful API design
- ✅ Exception handling and logging
- ✅ Object mapping with AutoMapper

## 🚀 Future Enhancements

- [ ] Add comprehensive unit tests (xUnit)
- [ ] Add integration tests
- [ ] Implement pagination and filtering
- [ ] Add Redis caching layer
- [ ] Implement advanced search functionality
- [ ] Add file upload for product images
- [ ] Implement email notifications
- [ ] Add payment gateway integration
- [ ] Docker containerization
- [ ] CI/CD pipeline setup

## 📄 License

This project is open source and available under the MIT License.

## 👨‍💻 Author

Created as a demonstration of enterprise-level ASP.NET Core API development.

## 📞 Support

For issues, questions, or suggestions, please open an issue on GitHub.

---

**Happy Coding! 🚀**
