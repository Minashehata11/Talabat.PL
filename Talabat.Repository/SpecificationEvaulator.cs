using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Specefication;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator<T> where T :BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> specs)
        {
            var query = inputQuery;

            if(specs.Criteria is not null)
                query=query.Where(specs.Criteria);
            if (specs.OrderBy is not null)
               query= query.OrderBy(specs.OrderBy);
            if(specs.OrderByDescending is not null)
              query=  query.OrderByDescending(specs.OrderByDescending);
            if(specs.IsPaginated)
                query= query.Skip(specs.Skip).Take(specs.Take); 
            query=specs.Includes.Aggregate(query,(CurrentQuery,IncludeExpression)=>CurrentQuery.Include(IncludeExpression));

            return query;   
        }

    }
}
