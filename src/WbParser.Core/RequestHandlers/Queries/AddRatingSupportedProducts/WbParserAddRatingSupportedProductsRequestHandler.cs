using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Interface.Queries.AddRatingSupportedProducts;

namespace WbParser.Core.RequestHandlers.Queries.AddRatingSupportedProducts
{
    public class WbParserAddRatingSupportedProductsRequestHandler : IConsumer<WbParserAddRatingSupportedProductsRequest>
    {
        private readonly ILogger<WbParserAddRatingSupportedProductsRequestHandler> _logger;
        private readonly IGenericRepository<RatingSupportedProductEntity> _ratingSupportedProductsRepo;
        private readonly IUnitOfWork _unitOfWork;
        
        public WbParserAddRatingSupportedProductsRequestHandler(ILogger<WbParserAddRatingSupportedProductsRequestHandler> logger,
            IGenericRepository<RatingSupportedProductEntity> ratingSupportedProductsRepo,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _ratingSupportedProductsRepo = ratingSupportedProductsRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<WbParserAddRatingSupportedProductsRequest> context)
        {
            var request = context.Message;
            try
            {
                var newItems = request.Articles.Select(x =>
                {
                    var newItem = new RatingSupportedProductEntity(x);

                    return _ratingSupportedProductsRepo.Add(newItem);
                });
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Не удалось добавить поддерживаемые артикулы для синхронизации рейтинга");
                throw;
            }
        }
    }
}