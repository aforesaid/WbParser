using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WbParser.Interface.Command.SyncRecommendQueries;

namespace WbParser.Core.RequestHandlers.Command
{
    public class WbParserSyncRecommendQueriesRequestHandler : IConsumer<WbParserSyncRecommendQueriesCommand>
    {
        private readonly ILogger<WbParserSyncRecommendQueriesRequestHandler> _logger;

        public WbParserSyncRecommendQueriesRequestHandler(ILogger<WbParserSyncRecommendQueriesRequestHandler> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WbParserSyncRecommendQueriesCommand> context)
        {
            try
            {
                await Handle();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0}:Не удалось выполнить рекомендованные запросы", 
                    nameof(WbParserSyncRecommendQueriesRequestHandler));
                throw;
            }
        }

        public async Task Handle()
        {
            throw new NotImplementedException();
        }
    }
}