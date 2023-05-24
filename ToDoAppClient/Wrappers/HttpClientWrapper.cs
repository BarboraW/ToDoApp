using ToDoAppClient.Models;

namespace ToDoAppClient.Wrappers
{
    public class HttpClientWrapper: IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, TaskModel model)
        {
            return await _httpClient.PostAsJsonAsync(requestUri, model);
        }
    }
}
