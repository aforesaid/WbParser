using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WbParser.Core.Services.Api.WbPublicApi.Requests.EmulateQueryByText;
using WbParser.Core.Services.Api.WbPublicApi.Requests.GetProductsByQuery;

namespace WbParser.Core.Services.Api.WbPublicApi
{
    public class WbPublicApiClient : BaseApiClient, IWbPublicApiClient
    {
       // private static readonly SemaphoreSlim Semaphore = new(100);
        
        public WbPublicApiClient()
        {
            HttpClient = new HttpClient();
        }

        public async Task<ApiProductByQuery[]> GetProductsByQuery(string text)
        {
            try
            {
                var result = new List<ApiProductByQuery>();

                var detailsByRequest = await EmulateQueryByText(text);
           
                if (detailsByRequest == null)
                    return null;

                ApiGetProductsByQueryResponse response;
                using var client = new HttpClient();
                
                var page = 1;
                const int limit = 100;
                const int maxCountPage = 100;
                do
                {
                    var url = WbPublicEndpoints.GetProductsRatingLink(detailsByRequest.ShardKey, detailsByRequest.Query,
                        page);

                    response = await Get<ApiGetProductsByQueryResponse>(url);

                    if (result.FirstOrDefault()?.Id == response.Data.Products.FirstOrDefault()?.Id)
                        break;

                    result.AddRange(response.Data.Products);
                    page++;
                } while (response.Data.Products.Length == limit && maxCountPage != page);
                return result.ToArray();            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ApiEmulateQueryByTextResponse> EmulateQueryByText(string text)
        {
            try
            {
                //await Semaphore.WaitAsync();

                var url = WbPublicEndpoints.EmulateQueryByText(text);

                var result = await Get<ApiEmulateQueryByTextResponse>(url);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
               // Semaphore.Release();
            }
        }

        public async Task<string[]> GetRecommendationQueriesByText(string text)
        {
            try
            {
               // await Semaphore.WaitAsync();

                var url = WbPublicEndpoints.GetRecommendationQueriesByText(text);

                var result = await Get<ApiEmulateQueryByTextResponse[]>(url);

                return result.Select(x => x.Name)
                    .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось получить информацию по рекомендованным запросам, {0}", e);
                throw;
            }
            finally
            {
              //  Semaphore.Release();
            }
        }
    }
}