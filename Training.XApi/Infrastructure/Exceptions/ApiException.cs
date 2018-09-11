using System;
using System.Net;
using Yokozuna.Logging.Extensions;

namespace Training.XApi.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public LogTags LogTags { get; internal set; }

        public ApiException(string url, HttpStatusCode? statusCode, string body, bool timeout, object data, string message)
            : base(message)
        {

            LogTags = new LogTags()
            {
                { "Data", data },
                { "StatusCode", (statusCode.HasValue ? statusCode.Value.ToString() : "Unknown") },
                { "Url", url },
                { "ResponseBody", body },
                { "Timeout", timeout.ToString() }
            };
        }

        public ApiException(string url, HttpStatusCode? statusCode, string body, bool timeout, WebExceptionStatus webExceptionStatus, object data, string message)
            : base(message)
        {
            var obj = new
            {
                Data = data,
                StatusCode = (statusCode.HasValue ? statusCode.Value.ToString() : "Unknown"),
                Url = url,
                ResponseBody = body,
                Timeout = timeout.ToString(),
                WebExceptionStatus = webExceptionStatus.ToString()
            };

            LogTags = new LogTags().Add(obj);
        }
    }
}
