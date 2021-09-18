using System.Threading.Tasks;
using WbParser.Core.Models.Managers;

namespace WbParser.Core.Services.Managers.RecommendQueryManager
{
    /// <summary>
    /// Автонакрутка поисковых запросов до необходимого уровня
    /// </summary>
    public interface IWbRecommendQueryManager
    {
        /// <summary>
        /// Запуск накрутки поискового запроса
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task StartWorkWithQuery(RecommendQueryGoal request);
        /// <summary>
        /// Получение запросов по их состоянию
        /// </summary>
        /// <returns></returns>
        Task<RecommendQueryGoal[]> GetQueriesWithCompletedState(bool completed);

        /// <summary>
        /// Синк всех запросов с количеством в countForOneQuery
        /// </summary>
        /// <param name="countForOneQuery">Количество раз, которое выполнится запрос</param>
        /// <returns></returns>
        Task SyncQueries(int countForOneQuery);
    }
}