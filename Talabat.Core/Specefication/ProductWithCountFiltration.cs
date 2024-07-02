using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class ProductWithCountFiltration:BaseSpecification<Product>
    {
        public ProductWithCountFiltration(SpecParameter specParameter)
           : base(
                p =>
                 (string.IsNullOrEmpty(specParameter.Search) || p.Name.ToLower().Trim().Contains(specParameter.Search))
                 &&
                (!specParameter.BrandId.HasValue || p.ProductBrandId == specParameter.BrandId)
                &&
                (!specParameter.TypeId.HasValue || p.ProductTypeId == specParameter.TypeId)
            )
        {
           
        }
    }
}
