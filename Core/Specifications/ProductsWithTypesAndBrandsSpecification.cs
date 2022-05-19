using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification
    : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }


/*  bu method calistiginda BaseSpecification sinifindaki asagidaki method calisiyor
    public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        } */
        public ProductsWithTypesAndBrandsSpecification(int id)
         : base(x => x.Id == id)
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}