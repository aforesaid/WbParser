using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WbParser.Core.Services.Api.WbPublicApi.Requests.EmulateQueryByText
{
    public sealed class ApiEmulateQueryByTextResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("shardKey")]
        public string ShardKey { get; set; }

        [JsonProperty("filters")]
        public string Filters { get; set; }
    }
}