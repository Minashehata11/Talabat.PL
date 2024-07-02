using Talabat.Core.Entities;
using Talabat.Core.Specefication;

namespace Talabat.Core
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Without Spec
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        #endregion
        #region With Specs

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecsAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecsAsync(ISpecification<T> spec);
        #endregion

    }
}
