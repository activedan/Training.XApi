using Training.XApi.Infrastructure.CQRS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Adverts;

namespace Training.XApi.Engine.Handlers.Queries.Adverts
{
    public static partial class QueryExtensions
    {
        public static Task<IEnumerable<Advert>> GetAdvertsByMemberId(this IQueryDispatcher dispatcher, Guid memberId)
        {
            return dispatcher.RequestAsync<AdvertsByMemberIdQuery, IEnumerable<Advert>>(new AdvertsByMemberIdQuery(memberId));
        }
    }

    internal class AdvertsByMemberIdQuery
    {
        public Guid MemberId { get; private set; }

        public AdvertsByMemberIdQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }

    internal class AdvertsByMemberIdQueryHandler : IQueryHandlerAsync<AdvertsByMemberIdQuery, IEnumerable<Advert>>
    {
        private ILogger<AdvertsByMemberIdQueryHandler> _logger;
        private IQueryDispatcher _queryDispatcher;

        public AdvertsByMemberIdQueryHandler(ILogger<AdvertsByMemberIdQueryHandler> logger, IQueryDispatcher queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<IEnumerable<Advert>> HandleAsync(AdvertsByMemberIdQuery query)
        {
            return null;
        }
    }
}
