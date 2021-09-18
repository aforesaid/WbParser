using System.Text;
using System.Web;

namespace WbParser.Core.Services.Api.WbPublicApi
{
    public static class WbPublicEndpoints
    {
        public static string EmulateQueryByText(string text)
        {
            var basePartOfUrl = "https://wbxsearch.wildberries.ru/exactmatch/v2/common?query=";
            var htmlPart = HttpUtility.UrlEncode(text);

            var result = basePartOfUrl + htmlPart;

            return result;
        }
        public static string GetRecommendationQueriesByText(string text)
        {
            const string basePartOfUrl = "https://wbxsearch.wildberries.ru/suggests/common?query=";
            var htmlPart = HttpUtility.UrlEncode(text);

            var result = basePartOfUrl + htmlPart;

            return result;
        }

        public static string GetProductsRatingLink(string sharedKey, string query, int page)
        {
            const string basePartOfUrl = "https://wbxcatalog-ru.wildberries.ru";
           
            var url = $"{basePartOfUrl}/{sharedKey}/catalog?locale=ru&{query}&page={page}";
            return url;
        }
    }
}