
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.Http
{
    public interface IHttpClient
    {
        HttpClientResponse<T> Execute<T>(HttpClientRequest request);

        HttpClientResponse Execute(HttpClientRequest request);

        Task<HttpClientResponse<T>> ExecuteAsync<T>(HttpClientRequest request);

        Task<HttpClientResponse> ExecuteAsync(HttpClientRequest request);
    }
}
