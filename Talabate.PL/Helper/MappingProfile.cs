using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabate.PL.Dtos;

namespace Talabate.PL.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                  .ForMember(d => d.productBrand, o => o.MapFrom(p => p.productBrand.Name))
                  .ForMember(d => d.ProductType, o => o.MapFrom(p => p.ProductType.Name))
                  .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<Address, AddressDto>();
            }
    }
}
