using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Specefication;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Product))
              return (IEnumerable<T>)  await _storeDbContext.Set<Product>().Include(p=>p.productBrand).Include(p=>p.ProductType).ToListAsync();
          return await _storeDbContext.Set<T>().ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            =>  await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await _storeDbContext.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithSpecsAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountWithSpecsAsync(ISpecification<T> spec)
         => await ApplySpecification(spec).CountAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            =>  SpecificationEvaluator<T>.GetQuery(_storeDbContext.Set<T>(), spec);
    }
}
