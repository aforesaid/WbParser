using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WbParser.Core.Models.Managers;
using WbParser.Core.Services.Api.WbPublicApi;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Infrastructure.Data.Repositories;

namespace WbParser.Core.Services.Managers.RecommendQueryManager
{
    public class WbRecommendQueryManager : IWbRecommendQueryManager
    {
        private readonly IWbPublicApiClient _wbPublicApiClient;

        private readonly RecommendQueryRepositoryManager _recommendQueryRepositoryManager;
        private readonly IGenericRepository<RecommendQueryEntity> _genericQueryRepository;

        public WbRecommendQueryManager(IWbPublicApiClient wbPublicApiClient, 
            RecommendQueryRepositoryManager recommendQueryRepositoryManager,
            IGenericRepository<RecommendQueryEntity> genericQueryRepository)
        {
            _wbPublicApiClient = wbPublicApiClient;
            _recommendQueryRepositoryManager = recommendQueryRepositoryManager;
            _genericQueryRepository = genericQueryRepository;
        }   

        public async Task StartWorkWithQuery(RecommendQueryGoal request)
        {
            try
            {
                var existQuery = await _recommendQueryRepositoryManager.GetByFullQuery(request.Query, request.SubQuery);
                
                if (existQuery == null)
                {
                    var newQuery =
                        new RecommendQueryEntity(request.Query, request.SubQuery, request.CurrentCount, false);
                    _genericQueryRepository.Add(newQuery);
                    await _recommendQueryRepositoryManager.UnitOfWork.SaveChangesAsync();
                }
                else
                {
                    if (!existQuery.Completed)
                    {
                        throw new ArgumentException("Запрос уже существует в списке запросов");
                    }
                    else
                    {
                        existQuery.SetActive();
                        _genericQueryRepository.Update(existQuery);
                        await _recommendQueryRepositoryManager.UnitOfWork.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<RecommendQueryGoal[]> GetQueriesWithCompletedState(bool completed)
        {
            try
            {
                var queries = await _recommendQueryRepositoryManager.GetWithSelectedState(completed)
                    .Select(x =>
                        new RecommendQueryGoal(x.Query, x.SubQuery, x.CurrentCount, x.Completed))
                    .ToArrayAsync();
                return queries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task SyncQueries(int countForOneQuery)
        {
            try
            {
                var existQueries = _recommendQueryRepositoryManager.GetWithSelectedState(completed: false);
                foreach (var existQuery in existQueries)
                {
                    var tasks = Enumerable.Range(0, countForOneQuery)
                        .Select(async _ => await _wbPublicApiClient.EmulateQueryByText(existQuery.ResultQuery))
                        .ToArray(); 
                    
                    await Task.WhenAll(tasks);
                    
                    existQuery.AddCurrentCount(countForOneQuery);
                    _genericQueryRepository.Update(existQuery);
                    await _recommendQueryRepositoryManager.UnitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}