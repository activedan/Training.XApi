using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.CQRS
{
    public interface IQueryHandler<TQuery, TResponse>
    {
        TResponse Handle(TQuery query);
    }

    public interface IQueryHandlerAsync<TQuery, TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
