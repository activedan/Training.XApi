using Training.XApi.Infrastructure.CQRS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Adverts;
using Training.XApi.Infrastructure.Http;
using System.Net;

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
        private IQueryDispatcher _queryDispatcher;
        private readonly IHttpClient _http;

        public AdvertsByMemberIdQueryHandler(IQueryDispatcher queryDispatcher, IHttpClient http)
        {
            _http = http;
            _queryDispatcher = queryDispatcher;
        }

        public async Task<IEnumerable<Advert>> HandleAsync(AdvertsByMemberIdQuery query)
        {
            IEnumerable<Advert> advert = new List<Advert>();

            var url = string.Format("", query.MemberId);
            var request = HttpClientRequest.Get(url);

            var response = await _http.ExecuteAsync<List<Advert>>(request);

            
            if (response != null && response.StatusCode == HttpStatusCode.OK && response.Result != null)
            {
                advert = response.Result;
            }

            return advert;
        }
    }
}
