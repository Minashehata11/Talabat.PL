using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context)
        {
            if(!context.productBrands.Any())
            {
            var brandType = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandType);

            if(brands?.Count>0) 
            {
                foreach(var brand in brands)
                    await context.Set<ProductBrand>().AddAsync(brand);
                await context.SaveChangesAsync();   
            }

            }
            
            if(!context.productTypes.Any()) 
            {  var Type = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(Type);

            if (types?.Count > 0)
            {
                foreach (var type in types)
                    await context.Set<ProductType>().AddAsync(type);
                await context.SaveChangesAsync();
            }

            }


            if (!context.Products.Any())
            {  var Product = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(Product);

            if (products?.Count > 0)
            {
                foreach (var product in products)
                    await context.Set<Product>().AddAsync(product);
                await context.SaveChangesAsync();
            }

            }

          
        }
    }
}
