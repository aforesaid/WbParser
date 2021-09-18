using System.ComponentModel.DataAnnotations;

namespace WbParser.Core.Models.Managers
{
    public class RecommendQueryGoal
    {
        public const int LimitCount = 1000000;
        public RecommendQueryGoal()
        { }
        public RecommendQueryGoal(string query, 
            string subQuery,
            int currentCount,
            bool completed)
        {
            Query = query;
            SubQuery = subQuery;
            CurrentCount = currentCount;
            Completed = completed;    
        }

        /// <summary>
        /// Основной поисковый запрос
        /// </summary>
        [Required]
        public string Query { get; set; }
        /// <summary>
        /// Подзапрос, который необходимо добавить к основному поисковому запросу
        /// </summary>
        [Required]
        public string SubQuery { get; set; }
        /// <summary>
        /// Текущее количество вызовов этого подзхапроса
        /// </summary>
        public int CurrentCount { get; set; }
        /// <summary>
        /// Результирующий запрос, который накручивается на Wb
        /// </summary>
        public string ResultQuery => $"{Query} {SubQuery}";
        
        public bool Completed { get; set; }
        /// <summary>
        /// Инкремент количества выполненных запросов
        /// </summary>
        /// <returns></returns>
        public bool AddCurrentCount()
        {
            if (CurrentCount >= LimitCount) 
                return false;
            
            CurrentCount += 1;
            return true;
        }
    }
}