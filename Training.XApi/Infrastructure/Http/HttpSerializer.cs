namespace Training.XApi.Infrastructure.Http
{
    public interface IHttpRequestSerialiser
    {
        string Serialise(object o);

        string ContentType { get; }
    }

    public interface IHttpResponseSerialiser
    {
        T Deserialise<T>(string text);

        string AcceptType { get; }
    }
}
