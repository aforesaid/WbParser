using System.ComponentModel.DataAnnotations;

namespace WbParser.Domain.Entities
{
    public class RecommendQueryEntity : Entity
    {
        private const int LimitCount = 1000000;
        
        private const int QueryLength = 128;
        private const int SubQueryLength = 64;
        
        private RecommendQueryEntity()
        { }
        public RecommendQueryEntity(string query, 
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
        [StringLength(QueryLength)]
        [Required]
        public string Query { get; private set; }
        /// <summary>
        /// Подзапрос, который необходимо добавить к основному поисковому запросу
        /// </summary>
        [StringLength(SubQueryLength)]
        [Required]
        public string SubQuery { get; private set; }
        /// <summary>
        /// Текущее количество вызовов этого подзхапроса
        /// </summary>
        public int CurrentCount { get; private set; }
        /// <summary>
        /// Результирующий запрос, который накручивается на Wb
        /// </summary>
        public string ResultQuery => $"{Query} {SubQuery}";
        
        public bool Completed { get; private set; }
        /// <summary>
        /// Инкремент количества выполненных запросов
        /// </summary>
        /// <returns></returns>
        public bool AddCurrentCount(int count)
        {
            if (CurrentCount + count >= LimitCount)
            {
                SetCompleted();
                return false;
            }

            CurrentCount += count;
            return true;
        }
        /// <summary>
        /// Задание неактивного статуса
        /// </summary>
        public void SetCompleted()
        {
            Completed = true;
            SetUpdated();
        }

        public void SetActive()
        {
            Completed = false;
            CurrentCount = 0;
            SetUpdated();
        }
    }
}