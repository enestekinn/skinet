using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    
    //when we finished , our transaction  is going to dispose of our context
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        // number of changes our database
        Task<int> Complete();
    }
}