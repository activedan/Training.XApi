using System.Collections.Generic;
using System.Net;

namespace Training.XApi.Infrastructure.Http
{
    public class AcceptType
    {
        public const string ApplicationJson = "application/json";
    }

    public class ContentType
    {
        public const string ApplicationJson = "application/json";
        public const string Form = "application/x-www-form-urlencoded";
        public const string Xml = "text/xml";
        public const string Jpg = "image/jpeg";
        public const string Stream = "application/octet-stream";
    }

    public class HttpClientRequest
    {
        internal object Body { get; set; }

        public string Url { get; protected set; }

        public string Method { get; protected set; }

        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The content type 
        /// </summary>
        public string ContentType { get; set; }

        public bool FollowRedirects { get; set; }

        public int? TimeoutInMS { get; set; }

        public string Accept { get; set; }

        public string UserAgent { get; set; }

        public CookieContainer Cookies { get; set; }

        protected HttpClientRequest()
        {
            TimeoutInMS = 10000;
            Headers = new Dictionary<string, string>();
        }

        protected void SetBody(object body)
        {
            Body = body;
        }

        public static HttpClientRequest Get(string url, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "GET";
            request.Accept = acceptType;

            return request;
        }

        public static HttpClientRequest Delete(string url, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "DELETE";
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }

        public static HttpClientRequest Put<T>(string url, T body, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "PUT";
            request.Body = body;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }

        public static HttpClientRequest Put<T>(string url, byte[] bytes, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "PUT";
            request.Body = bytes;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }

        public static HttpClientRequest Post<T>(string url, T body, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "POST";
            request.Body = body;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }

        public static HttpClientRequest Post(string url, byte[] bytes, string contentType = Http.ContentType.ApplicationJson, string acceptType = Http.AcceptType.ApplicationJson)
        {
            HttpClientRequest request = new HttpClientRequest();
            request.Url = url;
            request.Method = "POST";
            request.Body = bytes;
            request.ContentType = contentType;
            request.Accept = acceptType;

            return request;
        }
    }
}
