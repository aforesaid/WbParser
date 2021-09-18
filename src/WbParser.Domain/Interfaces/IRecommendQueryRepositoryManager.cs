using System.Linq;
using System.Threading.Tasks;
using WbParser.Domain.Entities;

namespace WbParser.Domain.Interfaces
{
    public interface IRecommendQueryRepositoryManager
    {
        IQueryable<RecommendQueryEntity> GetWithSelectedState(bool completed);
        IQueryable<RecommendQueryEntity> GetByQuery(string query);
        IQueryable<RecommendQueryEntity> GetBySubQuery(string subQuery);
        Task<RecommendQueryEntity> GetByFullQuery(string query, string subQuery);
    }
}