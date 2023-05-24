using ToDoAppClient.Models;

namespace ToDoAppClient.Wrappers
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsJsonAsync(string requestUri, TaskModel model);
    }
}
