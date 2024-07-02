using AutoMapper;
using Talabat.Core.Entities;
using Talabate.PL.Dtos;

namespace Talabate.PL.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApibaseUrl"]}{source.PictureUrl}";
            else
                return string.Empty ;   
        }
    }
}
