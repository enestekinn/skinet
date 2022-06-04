using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly StoreContext _context;
        
        // any repositories we are going to use inside uow are going to be stored inside hastable
        private Hashtable _repositories;
        
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();
            var type = typeof(TEntity).Name;
            // our hastable is going to store all of the repositories in use inside uow
            if (!_repositories.ContainsKey(type))
            {
                /*
                 * rather than using or creating an instance of our context, when we create our repository
                 * we are going to be passing in the context to our uow owns as a paramater into that  repository
                 * we are creating here
                 */
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator
                    .CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type,repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];

        }

        public  async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}