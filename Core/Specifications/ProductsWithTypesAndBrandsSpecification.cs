using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification
    : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(
           //string sort,int? brandId,int? typeId
           ProductSpecParams productParams
            )
        : base ( x=> 
        (string.IsNullOrEmpty(productParams.Search) ||  x.Name.ToLower().Contains(productParams.Search)) && 
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) 
        && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) 
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            // neden -1  koyduk.
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1),
            productParams.PageSize);

            AddOrderBy(x => x.Name); // OrderByName

            if(!string.IsNullOrEmpty(productParams.Sort)){
                switch(productParams.Sort){
                    case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                    case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                    default:
                    AddOrderBy(n => n.Name);
                    break;
                }
            }
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

//Bir seyi filtreleme yaparken Where Clause kullaniyoruz
