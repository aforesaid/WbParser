namespace WbParser.Interface.Queries.AddRatingSupportedProducts
{
    public class WbParserAddRatingSupportedProductsRequest
    {
        public WbParserAddRatingSupportedProductsRequest()
        { }

        public WbParserAddRatingSupportedProductsRequest(long[] articles)
        {
            Articles = articles;
        }
        /// <summary>
        /// Добавление Id артикулов
        /// </summary>
        public long[] Articles { get; set; }
    }
}