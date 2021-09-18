using Newtonsoft.Json;

namespace WbParser.Core.Services.Api.WbPublicApi.Requests.GetProductsByQuery
{
    public class ApiGetProductsByQueryData
    {
        [JsonProperty("products")] 
        public ApiProductByQuery[] Products { get; set; }
    }
}