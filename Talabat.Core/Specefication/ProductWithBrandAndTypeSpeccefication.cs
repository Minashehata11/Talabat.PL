using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class ProductWithBrandAndTypeSpecification:BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(SpecParameter specParameter )
            :base(
                 p=>
                 (string.IsNullOrEmpty(specParameter.Search) || p.Name.ToLower().Trim().Contains(specParameter.Search))
                 &&
                 (!specParameter.BrandId.HasValue || p.ProductBrandId== specParameter.BrandId)
                 &&
                 (!specParameter.TypeId.HasValue || p.ProductTypeId== specParameter.TypeId)
             )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(specParameter.Sort))
            {
                switch (specParameter.Sort)
                {
                    case "Price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDes":
                        AddOrderByDesc(p => p.Price);
                        break;
                    case "NameDes":
                        AddOrderByDesc(p => p.Name);

                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            AddPaginated(specParameter.PageSize*(specParameter.PageIndex-1),specParameter.PageSize);    
        }
        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
