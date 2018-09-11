using Training.XApi.Infrastructure.Http;
using Newtonsoft.Json;

namespace Training.XApi.Infrastructure.Serialisers
{
    internal class JsonHttpSerialiser : IHttpResponseSerialiser, IHttpRequestSerialiser
    {
        public string AcceptType
        {
            get
            {
                return "application/json";
            }
        }

        public string ContentType
        {
            get
            {
                return "application/json";
            }
        }

        public T Deserialise<T>(string text)
        {
            return (T)JsonConvert.DeserializeObject(text, typeof(T));
        }

        public string Serialise(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}
