using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    
    public class BaseSpecification<T> : ISpecification<T>
    {
          public BaseSpecification() 
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

      

        public Expression<Func<T, bool>> Criteria {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = 
        new List<Expression<Func<T, object>>>(); //emtpy list by default



// Where.Include() yaptigimizi  burasi yapacak
        protected void AddInclude(Expression<Func<T,object>> includeExpression){
            Includes.Add(includeExpression);
        }
    }
}

/* 
Buradaki tum olay  Repositorydeki Include methodlarini kaldirip
ToListAsync( -- ) icine gondermek. 
 */