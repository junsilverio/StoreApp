using AutoMapper;
using StoreApp.Application.DTOs;
using StoreApp.Domain.Entities;

namespace StoreApp.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Staff, StaffDto>();
            CreateMap<StaffDto, Staff>();

            // Stock.BrandId maps to Store_Id column
            CreateMap<Stock, StockDto>()
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.BrandId));
            CreateMap<StockDto, Stock>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.StoreId));

            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();
        }
    }
}
