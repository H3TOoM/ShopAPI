using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShopAPI.Data;
using ShopAPI.Repoistires;
using ShopAPI.Repoistires.Base;
using ShopAPI.Services;
using ShopAPI.Services.Base;

var builder = WebApplication.CreateBuilder(args);



// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Repoistory && Unit Of Work
builder.Services.AddScoped(typeof(IMainRepoistory<>), typeof(MainRepoistory<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IProductService, ProductService>()
        .AddScoped<IOrderService, OrderService>()
        .AddScoped<ICategoryService, CategoryService>()
        .AddScoped<IOrderItemService, OrderItemService>()
        .AddScoped<ICartItemService, CartItemService>()
        .AddScoped<IAccountService, AccountService>()
        .AddScoped<ICartService, CartService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IAddressService, AddressService>();


// Register Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
