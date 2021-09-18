using Newtonsoft.Json;

namespace WbParser.Core.Services.Api.WbPublicApi.Requests.GetProductsByQuery
{
    public class ApiProductByQuery
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("root")]
        public long Root { get; set; }

        [JsonProperty("kindId")] 
        public long KindId { get; set; }

        [JsonProperty("subjectId")] 
        public long SubjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("brand")] 
        public string Brand { get; set; }

        [JsonProperty("brandId")] 
        public long BrandId { get; set; }

        [JsonProperty("siteBrandId")] 
        public long SiteBrandId { get; set; }

        [JsonProperty("sale")] 
        public long Sale { get; set; }

        [JsonProperty("priceU")] 
        public long PriceU { get; set; }

        [JsonProperty("salePriceU")] 
        public long SalePriceU { get; set; }

        [JsonProperty("pics")] 
        public long Pics { get; set; }

        [JsonProperty("rating")] 
        public long Rating { get; set; }

        [JsonProperty("feedbacks")] 
        public long Feedbacks { get; set; }

        [JsonProperty("diffPrice")] 
        public bool DiffPrice { get; set; }

        [JsonProperty("isNew", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsNew { get; set; }

        [JsonProperty("promopic", NullValueHandling = NullValueHandling.Ignore)]
        public long? Promopic { get; set; }
    }
}