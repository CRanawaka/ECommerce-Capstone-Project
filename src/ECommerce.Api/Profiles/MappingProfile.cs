using AutoMapper;
using ECommerce.Api.Dtos;
using ECommerce.Core.Entities;

namespace ECommerce.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductCreateDto, Product>();

        }
    }
}
