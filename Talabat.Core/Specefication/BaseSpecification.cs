using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specefication
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } =new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Take { get; set; }   
        public int Skip { get; set; }
        public bool IsPaginated { get; set; } = false;

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria; 
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy= orderBy;           
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescending)
        {
            OrderBy = orderByDescending;
        }
        public void AddPaginated(int skip,int take) 
        { 
            IsPaginated= true;
            Skip= skip;
            Take= take;
        }

    }
}
