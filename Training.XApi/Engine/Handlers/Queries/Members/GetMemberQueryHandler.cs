using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Training.XApi.Engine.Models.Members;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Exceptions;
using Training.XApi.Infrastructure.Http;
using Yokozuna.Logging.Extensions;

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
        private readonly ILogger _logger;

        private readonly string _memberApiUrl;

        public GetMemberQueryHandler(ISettings settings, IHttpClient http, ILogger<GetMemberQueryHandler> logger)
        {
            _http = http;
            _logger = logger;

            _memberApiUrl = $"{settings.MemberApiUrl}";
        }

        public async Task<MemberProfile> HandleAsync(GetMemberQuery query)
        {
            try
            {
                string url = $"{_memberApiUrl}/v2/members/{query.MemberId}/profile";

                var request = HttpClientRequest.Get(url);

                var response = await _http.ExecuteAsync<MemberProfile>(request);

                if (response != null && response.StatusCode == HttpStatusCode.OK && response.Result != null)
                {
                    return response.Result;
                }
                else
                {
                    throw new ApiException(url, response.StatusCode, response.Body, response.IsTimeout, new LogTags().Add(query), $"Unable to get member data : {query.MemberId}");
                }
            }
            catch (ApiException exception)
            {
                _logger.Error(exception, exception.LogTags, exception.Message);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, new LogTags().Add(query), exception.Message);
            }

            return null;
        }
    }
}
