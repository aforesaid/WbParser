using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WbParser.Infrastructure.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingByQueryEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<long>(type: "bigint", nullable: false),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    Root = table.Column<long>(type: "bigint", nullable: false),
                    KindId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    BrandId = table.Column<long>(type: "bigint", nullable: false),
                    SiteBrandId = table.Column<long>(type: "bigint", nullable: false),
                    Sale = table.Column<long>(type: "bigint", nullable: false),
                    PriceU = table.Column<long>(type: "bigint", nullable: false),
                    SalePriceU = table.Column<long>(type: "bigint", nullable: false),
                    Pics = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<long>(type: "bigint", nullable: false),
                    Feedbacks = table.Column<long>(type: "bigint", nullable: false),
                    DiffPrice = table.Column<bool>(type: "boolean", nullable: false),
                    IsNew = table.Column<bool>(type: "boolean", nullable: true),
                    Promopic = table.Column<long>(type: "bigint", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingByQueryEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingSupportedProductEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<long>(type: "bigint", nullable: false),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingSupportedProductEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendQueries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Query = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SubQuery = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CurrentCount = table.Column<int>(type: "integer", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendQueries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncRatingQueryEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Inactive = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncRatingQueryEntities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingByQueryEntities_ArticleId",
                table: "RatingByQueryEntities",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingByQueryEntities_Brand",
                table: "RatingByQueryEntities",
                column: "Brand");

            migrationBuilder.CreateIndex(
                name: "IX_RatingByQueryEntities_IsNew",
                table: "RatingByQueryEntities",
                column: "IsNew");

            migrationBuilder.CreateIndex(
                name: "IX_RatingByQueryEntities_Name",
                table: "RatingByQueryEntities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RatingByQueryEntities_SalePriceU",
                table: "RatingByQueryEntities",
                column: "SalePriceU");

            migrationBuilder.CreateIndex(
                name: "IX_RatingSupportedProductEntities_ArticleId",
                table: "RatingSupportedProductEntities",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingSupportedProductEntities_Inactive",
                table: "RatingSupportedProductEntities",
                column: "Inactive");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendQueries_Completed",
                table: "RecommendQueries",
                column: "Completed");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendQueries_Query",
                table: "RecommendQueries",
                column: "Query");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendQueries_Query_SubQuery",
                table: "RecommendQueries",
                columns: new[] { "Query", "SubQuery" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendQueries_SubQuery",
                table: "RecommendQueries",
                column: "SubQuery");

            migrationBuilder.CreateIndex(
                name: "IX_SyncRatingQueryEntities_Name",
                table: "SyncRatingQueryEntities",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingByQueryEntities");

            migrationBuilder.DropTable(
                name: "RatingSupportedProductEntities");

            migrationBuilder.DropTable(
                name: "RecommendQueries");

            migrationBuilder.DropTable(
                name: "SyncRatingQueryEntities");
        }
    }
}
