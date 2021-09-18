using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;

namespace WbParser.Infrastructure.Data.Repositories
{
    public class RecommendQueryRepositoryManager : IRecommendQueryRepositoryManager
    {
        public readonly IUnitOfWork UnitOfWork;
        private readonly IGenericRepository<RecommendQueryEntity> _dbContext;

        public RecommendQueryRepositoryManager(IGenericRepository<RecommendQueryEntity> dbContext,
            IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public IQueryable<RecommendQueryEntity> GetWithSelectedState(bool completed)
        {
            return _dbContext.GetAll().Where(x => x.Completed == completed);
        }

        public IQueryable<RecommendQueryEntity> GetByQuery(string query)
        {
            return _dbContext.GetAll().Where(x => x.Query == query);
        }

        public IQueryable<RecommendQueryEntity> GetBySubQuery(string subQuery)
        {
            return _dbContext.GetAll().Where(x => x.SubQuery == subQuery);
        }

        public async Task<RecommendQueryEntity> GetByFullQuery(string query, string subQuery)
        {
            return await _dbContext.GetAll()
                .FirstOrDefaultAsync(x => x.Query == query &&
                                          x.SubQuery == subQuery);
        }
    }
}