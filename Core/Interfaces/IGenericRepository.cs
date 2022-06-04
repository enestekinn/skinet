using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
         Task<T> GetByIdAsync(int id);
         Task<IReadOnlyList<T>> ListAllAsync();
         Task<T> GetEntityWithSpec(ISpecification<T> spec);
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

         Task<int> CountAsync(ISpecification<T> spec);

         
         // methods are for uow pattern
         // they are not asynchronous methods the reason for this we are not going to be directly adding these to the 
         // database when we use any of these methods are always saying ef when we use these we want to add this  so track it or we want to update this so track it
         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
    }
}