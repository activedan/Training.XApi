using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.CQRS
{
    public interface IQueryDispatcher
    {
        TResponse Request<TQuery, TResponse>(TQuery query);

        Task<TResponse> RequestAsync<TQuery, TResponse>(TQuery query);
    }
}
