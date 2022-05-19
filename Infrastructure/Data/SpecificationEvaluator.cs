using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data 
{

/*
Bu methodlari yapiyor.    
 .Include(p => p.ProductType)
.Include(p => p.ProductBrand) 
*/
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
            {
var query = inputQuery;
if(spec.Criteria != null){
    query = query.Where(spec.Criteria); // p => p.ProductId == id
}
query = spec.Includes.Aggregate(
    query,(current,include) =>
     current.Include(include));
return query;
            }
    }
}//Aggreate methodu birden fazla include methodlarini birlestiriyor.