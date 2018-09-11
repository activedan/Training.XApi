using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Yokozuna.Logging.Extensions;

namespace Training.XApi.Infrastructure.CQRS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private ILogger _logger;
        private IServiceProvider _container;

        public QueryDispatcher(IServiceProvider container, ILogger<QueryDispatcher> logger)
        {
            _container = container;
            _logger = logger;
        }

        public TResponse Request<TQuery, TResponse>(TQuery query)
        {
            IQueryHandler<TQuery, TResponse> handler = null;
            try
            {
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
            catch (Exception exception)
            {
                _logger.Error(exception, new LogTags().Add(query), "Unable to handle query");
                throw;
            }
        }

        public Task<TResponse> RequestAsync<TQuery, TResponse>(TQuery query)
        {
            IQueryHandlerAsync<TQuery, TResponse> handler = null;

            try
            {
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
            catch (Exception exception)
            {
                _logger.Error(exception, new LogTags().Add(query), "Unable to handle async query");
                throw;
            }
        }
    }
}
