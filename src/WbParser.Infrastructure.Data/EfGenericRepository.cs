using System.Linq;
using WbParser.Domain.Interfaces;

namespace WbParser.Infrastructure.Data
{
    public class EfGenericRepository<T> : IGenericRepository<T> 
        where T : class
    {
        private readonly WbParserDbContext _dbContext;
        public EfGenericRepository(WbParserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public T Add(T obj)
        {
            return _dbContext.Set<T>().Add(obj).Entity;
        }

        public T Update(T obj)
        {
            _dbContext.Entry(obj).CurrentValues.SetValues(obj);
            return _dbContext.Set<T>().Update(obj).Entity;
        }
    }
}