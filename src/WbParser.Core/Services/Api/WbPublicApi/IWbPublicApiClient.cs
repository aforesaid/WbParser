using System.Threading.Tasks;
using WbParser.Core.Services.Api.WbPublicApi.Requests.EmulateQueryByText;
using WbParser.Core.Services.Api.WbPublicApi.Requests.GetProductsByQuery;

namespace WbParser.Core.Services.Api.WbPublicApi
{
    public interface IWbPublicApiClient
    {
        Task<ApiProductByQuery[]> GetProductsByQuery(string text);
        Task<ApiEmulateQueryByTextResponse> EmulateQueryByText(string text);
        Task<string[]> GetRecommendationQueriesByText(string text); 
    }
}