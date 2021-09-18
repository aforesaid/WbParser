namespace WbParser.Interface.Command.SyncRatingQueries
{
    public class WbParserSyncRatingQueriesCommand
    {
        public WbParserSyncRatingQueriesCommand()
        { }

        public WbParserSyncRatingQueriesCommand(string query)
        {
            Query = query;
        }
        /// <summary>
        /// Опциональный query, если null - синкается весь каталог
        /// </summary>
        public string Query { get; set; }
    }
}