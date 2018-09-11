using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private IServiceProvider _container;

        public QueryDispatcher(IServiceProvider container)
        {
            _container = container;
        }

        public TResponse Request<TQuery, TResponse>(TQuery query)
        {
            IQueryHandler<TQuery, TResponse> handler = null;

            handler = _container.GetService<IQueryHandler<TQuery, TResponse>>();

            if (handler == null)
            {
                throw new Exception("Unable to find matching query handler");
            }
            else
            {
                return handler.Handle(query);
            }

        }

        public Task<TResponse> RequestAsync<TQuery, TResponse>(TQuery query)
        {
            IQueryHandlerAsync<TQuery, TResponse> handler = null;

            handler = _container.GetService<IQueryHandlerAsync<TQuery, TResponse>>();

            if (handler == null)
            {
                throw new Exception("Unable to find matching async query handler");
            }
            else
            {
                return handler.HandleAsync(query);
            }
        }
    }
}
