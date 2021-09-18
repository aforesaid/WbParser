using System.Linq;

namespace WbParser.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> GetAll();
        T Add(T obj);
        T Update(T obj);
    }
}