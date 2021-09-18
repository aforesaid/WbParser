using Newtonsoft.Json;

namespace WbParser.Core.Services.Api.WbPublicApi.Requests.GetProductsByQuery
{
    public class ApiGetProductsByQueryResponse
    {
        [JsonProperty("state")] 
        public long State { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("data")]
        public ApiGetProductsByQueryData Data { get; set; }
    }
}