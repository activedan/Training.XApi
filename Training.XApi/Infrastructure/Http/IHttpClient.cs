
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.Http
{
    public interface IHttpClient
    {
        Task<HttpClientResponse<T>> ExecuteAsync<T>(HttpClientRequest request);
    }
}
