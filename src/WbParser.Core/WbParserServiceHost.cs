using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WbParser.Core.RequestHandlers.Command;
using WbParser.Core.RequestHandlers.Queries.AddRatingSupportedProducts;
using WbParser.Core.Services.Api.WbPublicApi;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Infrastructure.Data;
using WbParser.Infrastructure.Data.Repositories;
using WbParser.Interface.Command.SyncRatingQueries;
using WbParser.Interface.Queries.AddRatingSupportedProducts;

namespace WbParser.Core
{
    public class WbParserServiceHost
    {
        public ServiceProvider ServiceProvider { get; private set; }

        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        public WbParserServiceHost(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _serviceCollection = serviceCollection;
            _configuration = configuration;
        }

        public async Task Start()
        {
            RegisterRepositories();
            RegisterServices();
            AddDbContext();
            AddMassTransit();
            
            await ConfigureDbContext();
            
            ServiceProvider = _serviceCollection.BuildServiceProvider();
            
            var busControl = ServiceProvider.GetRequiredService<IBusControl>();
            await busControl.StartAsync();
        }

        public void AddDbContext()
        {
            _serviceCollection.AddDbContext<WbParserDbContext>(x =>
                x.UseNpgsql(_configuration["POSTGRESQL"], my => my.EnableRetryOnFailure()));
        }

        public async Task ConfigureDbContext()
        {
            ServiceProvider = _serviceCollection.BuildServiceProvider();
            var dbContext = ServiceProvider.GetRequiredService<WbParserDbContext>();

            await dbContext.Database.MigrateAsync();
            
            _serviceCollection.AddScoped<IUnitOfWork>(x => x.GetRequiredService<WbParserDbContext>());
        }

        public void RegisterServices()
        {
            _serviceCollection.AddScoped<IWbPublicApiClient, WbPublicApiClient>();
            _serviceCollection.AddScoped<IRecommendQueryRepositoryManager, RecommendQueryRepositoryManager>();
        }

        public void RegisterRepositories()
        {
            _serviceCollection
                .AddScoped<IGenericRepository<RatingByQueryEntity>, EfGenericRepository<RatingByQueryEntity>>();
            _serviceCollection
                .AddScoped<IGenericRepository<RecommendQueryEntity>, EfGenericRepository<RecommendQueryEntity>>();
            _serviceCollection
                .AddScoped<IGenericRepository<SyncRatingQueryEntity>, EfGenericRepository<SyncRatingQueryEntity>>();
            _serviceCollection
                .AddScoped<IGenericRepository<RatingSupportedProductEntity>,
                    EfGenericRepository<RatingSupportedProductEntity>>();
            
            _serviceCollection.AddScoped<RecommendQueryRepositoryManager>();
        }

        public void AddMassTransit()
        {
            _serviceCollection.AddMassTransit(busConfig =>
            {
                busConfig.AddConsumer<WbParserSyncRecommendQueriesRequestHandler>();
                busConfig.AddConsumer<WbParserSyncRatingQueriesRequestHandler>();
                busConfig.AddConsumer<WbParserAddRatingSupportedProductsRequestHandler>();

                busConfig.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(WbParserSyncRatingQueriesCommand.QueueName, e =>
                    {
                        e.Consumer<WbParserSyncRecommendQueriesRequestHandler>(context);
                        e.Consumer<WbParserSyncRatingQueriesRequestHandler>(context);

                    });
                    cfg.Host(_configuration["RABBIT_HOST"] ?? "rabbitmq", _configuration["RABBIT_APP"] ?? "/", host =>
                    {
                        host.Username(_configuration["RABBIT_USER"] ?? "guest");
                        host.Password(_configuration["RABBIT_PASS"] ?? "guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}