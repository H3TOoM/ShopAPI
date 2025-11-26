using AutoMapper;
using ShopAPI.DTOs;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ConfigureProductMaps();
            ConfigureCategoryMaps();
            ConfigureUserMaps();
            ConfigureAddressMaps();
            ConfigureCartMaps();
            ConfigureOrderMaps();
        }

        private void ConfigureProductMaps()
        {
            CreateMap<Product, ProductViewDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }

        private void ConfigureCategoryMaps()
        {
            CreateMap<Category, CategoryViewDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }

        private void ConfigureUserMaps()
        {
            CreateMap<User, UserViewDto>().ReverseMap();

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }

        private void ConfigureAddressMaps()
        {
            CreateMap<Address, AddressViewDto>().ReverseMap();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>();
        }

        private void ConfigureCartMaps()
        {
            CreateMap<CartItem, CartItemViewDto>().ReverseMap();
            CreateMap<CartItemCreateDto, CartItem>();
            CreateMap<CartItemUpdateDto, CartItem>();

            CreateMap<Cart, CartViewDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartViewDto, Cart>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CartCreateDto, Cart>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CartUpdateDto, Cart>()
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.Items));
        }

        private void ConfigureOrderMaps()
        {
            CreateMap<OrderItem, OrderItemViewDto>().ReverseMap();
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItemUpdateDto, OrderItem>();

            CreateMap<Order, OrderViewDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderViewDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));
        }
    }
}
