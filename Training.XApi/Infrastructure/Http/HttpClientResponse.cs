using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Training.XApi.Infrastructure.Http
{
    public class HttpClientResponse
    {
        public Byte[] Bytes { get; private set; }

        public HttpStatusCode? StatusCode { get; private set; }

        public string Body { get; private set; }

        public Dictionary<string, string> Headers { get; private set; }

        public bool IsTimeout { get; private set; }

        public WebExceptionStatus WebExceptionStatus { get; private set; }

        public HttpClientResponse(WebExceptionStatus webExceptionStatus)
        {
            WebExceptionStatus = webExceptionStatus;
            IsTimeout = webExceptionStatus == WebExceptionStatus.Timeout;
        }

        internal HttpClientResponse(WebExceptionStatus webExceptionStatus, HttpWebResponse httpWebResponse) : this(webExceptionStatus)
        {
            if (httpWebResponse == null)
            {
                return;
            }

            StatusCode = httpWebResponse.StatusCode;

            Headers = new Dictionary<string, string>();
            foreach (string key in httpWebResponse.Headers.Keys)
            {
                Headers.Add(key, httpWebResponse.Headers[key]);
            }

            using (var rs = httpWebResponse.GetResponseStream())
            {
                if (rs != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        rs.CopyTo(ms);

                        Bytes = ms.ToArray();
                        Body = Encoding.UTF8.GetString(Bytes);
                    }
                }
            }
        }
    }

    public class HttpClientResponse<T> : HttpClientResponse
    {
        public T Result { get; internal set; }

        public HttpClientResponse(WebExceptionStatus responseStatus)
            : base(responseStatus)
        {
        }

        internal HttpClientResponse(WebExceptionStatus responseStatus, HttpWebResponse httpWebResponse)
            : base(responseStatus, httpWebResponse)
        {
        }
    }
}
