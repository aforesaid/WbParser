using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WbParser.Core.Services.Api.WbPublicApi;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Interface.Command.SyncRatingQueries;

namespace WbParser.Core.RequestHandlers.Command
{
    public class WbParserSyncRatingQueriesRequestHandler : IConsumer<WbParserSyncRatingQueriesCommand>
    {
        private readonly ILogger<WbParserSyncRatingQueriesRequestHandler> _logger;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SyncRatingQueryEntity> _ratingQueriesEntityRepo;
        private readonly IGenericRepository<RatingSupportedProductEntity> _ratingSupportedProductEntityRepo;
        private readonly IGenericRepository<RatingByQueryEntity> _ratingByQueryEntityRepo;
        private readonly IWbPublicApiClient _wbPublicApiClient;

        public WbParserSyncRatingQueriesRequestHandler(ILogger<WbParserSyncRatingQueriesRequestHandler> logger,
            IUnitOfWork unitOfWork,
            IGenericRepository<SyncRatingQueryEntity> ratingQueriesEntityRepo, 
            IWbPublicApiClient wbPublicApiClient, 
            IGenericRepository<RatingSupportedProductEntity> ratingSupportedProductEntityRepo,
            IGenericRepository<RatingByQueryEntity> ratingByQueryEntityRepo)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _ratingQueriesEntityRepo = ratingQueriesEntityRepo;
            _wbPublicApiClient = wbPublicApiClient;
            _ratingSupportedProductEntityRepo = ratingSupportedProductEntityRepo;
            _ratingByQueryEntityRepo = ratingByQueryEntityRepo;
        }

        public async Task Consume(ConsumeContext<WbParserSyncRatingQueriesCommand> context)
        {
            var request = context.Message;
            try
            {
                await Handle(request.Query);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{0}:Не удалось синхронизировать рейтинг позиций, запрос: {1}",
                    nameof(WbParserSyncRatingQueriesRequestHandler), request.Query);
                throw;
            }
        }

        public async Task Handle(string query)
        {
            var supportedProducts = await _ratingSupportedProductEntityRepo.GetAll()
                .Where(x => !x.Inactive)
                .ToArrayAsync();
            
            var supportedArticleIds = supportedProducts.Select(x => x.ArticleId)
                .ToArray();
            
            if (query != null)
            {
                var existQuery = await _ratingQueriesEntityRepo.GetAll()
                    .Where(x => !x.Inactive)
                    .FirstOrDefaultAsync(x => x.Name == query);
                
                if (existQuery != null)
                {
                    await SyncRatingByQuery(query, supportedArticleIds);
                }
            }
            else
            {
                var queries = await _ratingQueriesEntityRepo.GetAll()
                    .ToArrayAsync();
                foreach (var item in queries)
                {
                    await SyncRatingByQuery(item.Name, supportedArticleIds);
                }
            }
        }

        private async Task SyncRatingByQuery(string query, long[] supportedArticleIds)
        {
            var ratingItems = await _wbPublicApiClient.GetProductsByQuery(query);

            var newExistItems = ratingItems
                .Select((item, index) => new {item, index})
                .Where(x => supportedArticleIds.Contains(x.item.Id))
                .Select(x =>
                {
                    var newItem = new RatingByQueryEntity(x.item.Id,
                        x.item.Root,
                        x.item.KindId,
                        x.item.SubjectId,
                        x.item.Name,
                        x.item.Brand,
                        x.item.BrandId,
                        x.item.SiteBrandId,
                        x.item.Sale,
                        x.item.PriceU,
                        x.item.SalePriceU,
                        x.item.Pics,
                        x.item.Rating,
                        x.item.Feedbacks,
                        x.item.DiffPrice,
                        x.item.IsNew,
                        x.item.Promopic,
                        x.index);
                    return _ratingByQueryEntityRepo.Add(newItem);
                }).ToArray();
            await _unitOfWork.SaveChangesAsync();
        }
    }
}