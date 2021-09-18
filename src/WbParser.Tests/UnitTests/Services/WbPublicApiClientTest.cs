using System.Linq;
using System.Threading.Tasks;
using WbParser.Core.Services.Api.WbPublicApi;
using Xunit;

namespace WbParser.Tests.UnitTests.Services
{
    public class WbPublicApiClientTest
    {
        private readonly WbPublicApiClient _apiClient;
        public WbPublicApiClientTest()
        {
            _apiClient = new WbPublicApiClient();
        }

        [Fact]
        public async Task EmulateQueryByTextText()
        {
            var text = "эспандер fitbuy";
            
            var result = await _apiClient.EmulateQueryByText(text);
            
            Assert.True(result.Name != null);
        }

        [Fact]
        public async Task GetRecommendationsQueriesByTextTest()
        {
            var text = "банты";

            var result = await _apiClient.GetRecommendationQueriesByText(text);
            
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetProductsByText()
        {
            var text = "банты";
            var result = await _apiClient.GetProductsByQuery(text);
            
            Assert.NotEmpty(result);
            Assert.NotNull(result.First().Brand);
            Assert.NotNull(result.First().Name);
            Assert.NotEqual(0, result.First().Feedbacks);
            Assert.NotEqual(0, result.First().Id);
            Assert.NotEqual(0, result.First().Root);
            Assert.NotEqual(0, result.First().PriceU);
            Assert.NotEqual(0, result.First().SalePriceU);
        }
    }
}