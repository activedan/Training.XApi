using Training.XApi.Infrastructure.CQRS;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Adverts;

namespace Training.XApi.Engine.Handlers.Queries.Adverts
{
    public class AdvertByIdQuery
    {
        public Guid AdvertId { get; private set; }

        public AdvertByIdQuery(Guid advertId)
        {
            AdvertId = advertId;
        }
    }    

    internal class AdvertByIdQueryHandler : IQueryHandlerAsync<AdvertByIdQuery, Advert>
    {
        private ILogger<AdvertByIdQueryHandler> _logger;

        public AdvertByIdQueryHandler(ILogger<AdvertByIdQueryHandler> logger)
        {
            _logger = logger;
        }

        public async Task<Advert> HandleAsync(AdvertByIdQuery query)
        {
            return null;
        }
    }
}
