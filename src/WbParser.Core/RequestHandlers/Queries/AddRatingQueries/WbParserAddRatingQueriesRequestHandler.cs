using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Interface.Queries.AddRatingQueries;

namespace WbParser.Core.RequestHandlers.Queries.AddRatingQueries
{
    public class WbParserAddRatingQueriesRequestHandler : IConsumer<WbParserAddRatingQueriesRequest>
    {
        private readonly ILogger<WbParserAddRatingQueriesRequestHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SyncRatingQueryEntity> _syncRatingQueriesRepo;

        public WbParserAddRatingQueriesRequestHandler(ILogger<WbParserAddRatingQueriesRequestHandler> logger,
            IGenericRepository<SyncRatingQueryEntity> syncRatingQueriesRepo,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _syncRatingQueriesRepo = syncRatingQueriesRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<WbParserAddRatingQueriesRequest> context)
        {
            try
            {
                var request = context.Message;
                await Handle(request.Items);
                await context.RespondAsync<WbParserAddRatingQueriesResponse>(new WbParserAddRatingQueriesResponse());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0}: Не удалось добавить синхронизируемые рейтинговые запросы",
                    nameof(WbParserAddRatingQueriesRequestHandler));
                throw;
            }
        }

        public async Task Handle(string[] queries)
        {
            var newQueries = queries.Select(x =>
            {
                var newItem = new SyncRatingQueryEntity(x);
                return _syncRatingQueriesRepo.Add(newItem);
            }).ToArray();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}