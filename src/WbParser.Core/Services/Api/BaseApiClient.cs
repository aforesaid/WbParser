using System;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace WbParser.Core.Services.Api
{
    public abstract class BaseApiClient : IDisposable
    {
        protected HttpClient HttpClient;

        protected async Task<TResult> Post<TRequest, TResult>(string url, TRequest request, string token = null)
        {
            SetAuthorize(token);
            var requestString = Serialize(request);
            var requestModel = new StringContent(requestString, Encoding.UTF8, "application/json");
            
            var result = await HttpClient.PostAsync(url, requestModel);
            
            try
            {
                var resultString = await result.Content.ReadAsStringAsync();
                
                var response = Deserialize<TResult>(resultString);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось выполнить запрос {0}", e.Message);
                throw;
            }
        }

        protected async Task<TResult> Get<TResult>(string url, string token = null)
        {
            SetAuthorize(token);

            var result = await HttpClient.GetAsync(url);
            
            try
            {
                var resultString = await result.Content.ReadAsStringAsync();
                
                var response = Deserialize<TResult>(resultString);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось выполнить запрос {0}", e.Message);
                throw;
            }
        }

        private void SetAuthorize(string token)
        {
            const string authorization = "Authorization";
            
            if (string.IsNullOrWhiteSpace(token)) 
                return;
            
            HttpClient.DefaultRequestHeaders.Remove(authorization);
            HttpClient.DefaultRequestHeaders.Add(authorization, token);
        }

        private static string Serialize<T>(T model)
        {
            var result = JsonSerializer.Serialize(model, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            });

            return result;
        }

        private static T Deserialize<T>(string content)
        {
            var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            });

            return result;
        }


        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}