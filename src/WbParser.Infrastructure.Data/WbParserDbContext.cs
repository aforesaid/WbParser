using Microsoft.EntityFrameworkCore;
using WbParser.Domain.Entities;
using WbParser.Domain.Interfaces;

namespace WbParser.Infrastructure.Data
{
    public class WbParserDbContext : DbContext, IUnitOfWork
    {
        public WbParserDbContext() : base()
        { }

        public WbParserDbContext(DbContextOptions<WbParserDbContext> options) :
            base(options)
        { }
        protected DbSet<RecommendQueryEntity> RecommendQueries { get; set; }
        protected DbSet<RatingByQueryEntity> RatingByQueryEntities { get; set; }
        protected DbSet<SyncRatingQueryEntity> SyncRatingQueryEntities { get; set; }
        protected DbSet<RatingSupportedProductEntity> RatingSupportedProductEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=root;Server=localhost;Port=5432;Database=wbparser;Integrated Security=true;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //RecommendQueryEntity
            modelBuilder.Entity<RecommendQueryEntity>().HasKey(x => x.Id);
           
            modelBuilder.Entity<RecommendQueryEntity>().HasIndex(x => x.Query);
            modelBuilder.Entity<RecommendQueryEntity>().HasIndex(x => x.SubQuery);
            modelBuilder.Entity<RecommendQueryEntity>().HasIndex(x => new {x.Query, x.SubQuery})
                .IsUnique();
            modelBuilder.Entity<RecommendQueryEntity>().HasIndex(x => x.Completed);
            
            //RatingByQueryEntity
            modelBuilder.Entity<RatingByQueryEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<RatingByQueryEntity>().HasIndex(x => x.Brand);
            modelBuilder.Entity<RatingByQueryEntity>().HasIndex(x => x.ArticleId);
            modelBuilder.Entity<RatingByQueryEntity>().HasIndex(x => x.Name);
            modelBuilder.Entity<RatingByQueryEntity>().HasIndex(x => x.IsNew);
            modelBuilder.Entity<RatingByQueryEntity>().HasIndex(x => x.SalePriceU);
            
            //SyncRatingQueryEntity
            modelBuilder.Entity<SyncRatingQueryEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<SyncRatingQueryEntity>().HasIndex(x => x.Name);
            
            //RatingSupportedProductEntities
            modelBuilder.Entity<RatingSupportedProductEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<RatingSupportedProductEntity>().HasIndex(x => x.ArticleId)
                .IsUnique();
            modelBuilder.Entity<RatingSupportedProductEntity>().HasIndex(x => x.Inactive);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}