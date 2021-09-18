using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WbParser.Core.Services.Api.WbPublicApi;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;
using WbParser.Infrastructure.Data;
using WbParser.Infrastructure.Data.Repositories;

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
            
            await ConfigureDbContext();
            
            ServiceProvider = _serviceCollection.BuildServiceProvider();
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
    }
}