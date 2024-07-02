using AutoMapper;
using Talabat.Core.Entities;

namespace Talabate.PL.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string productBrand { get; set; }
        public int ProductBrandId { get; set; }   //Foreign key
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }   //Foreign key

    }
}
