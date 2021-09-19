namespace WbParser.Interface.Queries.AddRatingQueries
{
    public class WbParserAddRatingQueriesRequest
    {
        public WbParserAddRatingQueriesRequest()
        { }

        public WbParserAddRatingQueriesRequest(string[] items)
        {
            Items = items;
        }
        public string[] Items { get; set; }
    }
}