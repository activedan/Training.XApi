using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Training.XApi.Infrastructure.Http
{
    public class HttpClient : IHttpClient
    {
        private IEnumerable<IHttpRequestSerialiser> _requestSerialisers;
        private IEnumerable<IHttpResponseSerialiser> _responseDeserialisers;

        public HttpClient(IEnumerable<IHttpRequestSerialiser> requestSerialisers, IEnumerable<IHttpResponseSerialiser> responseDeserialisers)
        {
            _requestSerialisers = requestSerialisers;
            _responseDeserialisers = responseDeserialisers;
        }

        public HttpClientResponse Execute(HttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = GetHttpWebRequest(request);
            HttpClientResponse response = null;


            HttpWebResponse httpWebResponse = Sync(httpWebRequest);

            response = new HttpClientResponse(WebExceptionStatus.Success, httpWebResponse);

            if (httpWebResponse != null)
            {
                httpWebResponse.Dispose();
            }

            return response;
        }

        public HttpClientResponse<T> Execute<T>(HttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = GetHttpWebRequest(request);
            HttpClientResponse<T> response = null;

            HttpWebResponse httpWebResponse = Sync(httpWebRequest);

            response = new HttpClientResponse<T>(WebExceptionStatus.Success, httpWebResponse);
            response.Result = GetResponseSerialiser(request.Accept).Deserialise<T>(response.Body);

            if (httpWebResponse != null)
            {
                httpWebResponse.Dispose();
            }

            return response;
        }

        public async Task<HttpClientResponse> ExecuteAsync(HttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = GetHttpWebRequest(request);
            HttpClientResponse response = null;

            HttpWebResponse httpWebResponse = await Async(httpWebRequest);

            response = new HttpClientResponse(WebExceptionStatus.Success, httpWebResponse);

            if (httpWebResponse != null)
            {
                httpWebResponse.Dispose();
            }

            return response;
        }

        public async Task<HttpClientResponse<T>> ExecuteAsync<T>(HttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = GetHttpWebRequest(request);
            HttpClientResponse<T> response = null;

            HttpWebResponse httpWebResponse = await Async(httpWebRequest);

            response = new HttpClientResponse<T>(WebExceptionStatus.Success, httpWebResponse);
            response.Result = GetResponseSerialiser(request.Accept).Deserialise<T>(response.Body);

            if (httpWebResponse != null)
            {
                httpWebResponse.Dispose();
            }

            return response;
        }

        private async Task<HttpWebResponse> Async(HttpWebRequest httpWebRequest)
        {
            try
            {
                return (HttpWebResponse)await httpWebRequest.GetResponseAsync();
            }
            catch (WebException wex)
            {
                if (wex.Response == null || wex.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                return HandleWebException(wex, httpWebRequest);
            }
        }

        private HttpWebResponse Sync(HttpWebRequest httpWebRequest)
        {
            try
            {
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException wex)
            {
                if (wex.Response == null || wex.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                return HandleWebException(wex, httpWebRequest);
            }
        }

        private HttpWebResponse HandleWebException(WebException exception, HttpWebRequest request)
        {
            HttpWebResponse httpWebResponse = null;
            WebExceptionStatus responseStatus = exception.Status;

            if (exception.Response != null)
            {
                httpWebResponse = (HttpWebResponse)exception.Response;
            }

            return httpWebResponse;
        }



        private HttpWebRequest GetHttpWebRequest(HttpClientRequest request)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);

            httpWebRequest.Method = request.Method;
            httpWebRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            httpWebRequest.Timeout = request.TimeoutInMS ?? 1000;

            httpWebRequest.AllowAutoRedirect = request.FollowRedirects;

            if (!string.IsNullOrWhiteSpace(request.Accept))
            {
                httpWebRequest.Accept = request.Accept;
            }

            foreach (var header in request.Headers)
            {
                httpWebRequest.Headers.Set(header.Key, header.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.ContentType) && request.Method.ToLower() != "get")
            {
                httpWebRequest.ContentType = request.ContentType;
            }

            if (!string.IsNullOrWhiteSpace(request.UserAgent))
            {
                httpWebRequest.UserAgent = request.UserAgent;
            }

            if (request.Cookies != null)
            {
                httpWebRequest.CookieContainer = request.Cookies;
            }

            if (request.Body != null)
            {
                byte[] body;

                if (request.Body is byte[])
                {
                    body = request.Body as byte[];
                }
                else
                {
                    body = Encoding.UTF8.GetBytes(GetRequestSerialiser(request.ContentType).Serialise(request.Body));
                }

                httpWebRequest.ContentLength = body == null ? 0 : body.Length;

                if (body != null)
                {
                    using (var rs = httpWebRequest.GetRequestStream())
                    {
                        rs.Write(body, 0, body.Length);
                    }
                }
            }

            return httpWebRequest;
        }

        private IHttpRequestSerialiser GetRequestSerialiser(string contentType)
        {
            IHttpRequestSerialiser serialiser = _requestSerialisers.FirstOrDefault(o => o.ContentType == contentType);

            if (serialiser == null)
            {
                throw new Exception("Unable to find matching request serialiser for content type: " + contentType);
            }

            return serialiser;
        }

        private IHttpResponseSerialiser GetResponseSerialiser(string acceptType)
        {
            IHttpResponseSerialiser serialiser = _responseDeserialisers.FirstOrDefault(o => o.AcceptType == acceptType);

            if (serialiser == null)
            {
                throw new Exception("Unable to find matching response deserialiser for accept type: " + acceptType);
            }

            return serialiser;
        }
    }
}
