using System;
using System.Net;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Members;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Http;

namespace Training.XApi.Engine.Handlers.Queries.Members
{
    public static partial class QueryExtensions
    {
        public static Task<MemberProfile> GetMemberById(this IQueryDispatcher queryDispatcher, Guid memberId)
        {
            return queryDispatcher.RequestAsync<GetMemberQuery, MemberProfile>(new GetMemberQuery(memberId));
        }
    }

    public class GetMemberQuery
    {
        public Guid MemberId { get; }
        public GetMemberQuery(Guid memberId)
        {
            MemberId = memberId;
        }
    }

    internal class GetMemberQueryHandler : IQueryHandlerAsync<GetMemberQuery, MemberProfile>
    {
        private readonly IHttpClient _http;

        private readonly string _memberApiUrl;

        public GetMemberQueryHandler(ISettings settings, IHttpClient http)
        {
            _http = http;

            _memberApiUrl = settings.MemberApiUrl;
        }

        public async Task<MemberProfile> HandleAsync(GetMemberQuery query)
        {
            string url = $"{_memberApiUrl}/v2/members/{query.MemberId}/profile";

            var request = HttpClientRequest.Get(url);

            var response = await _http.ExecuteAsync<MemberProfile>(request);

            if (response != null && response.StatusCode == HttpStatusCode.OK && response.Result != null)
            {
                return response.Result;
            }

            return null;
        }
    }
}
