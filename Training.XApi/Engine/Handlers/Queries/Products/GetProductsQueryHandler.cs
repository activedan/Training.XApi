using Training.XApi.Engine.Models.Products;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Training.XApi.Engine.Handlers.Queries.Products
{
    public class GetProductsQuery
    {
        public string Service { get; set; }

        public GetProductsQuery(string service)
        {
            Service = service;
        }
    }
    public class GetProductsQueryHandler : IQueryHandlerAsync<GetProductsQuery, List<Product>>
    {
        IHttpClient _http;

        private readonly string _productUrl;

        public GetProductsQueryHandler(IHttpClient http, ISettings settings)
        {
            _http = http;
        }

        public async Task<List<Product>> HandleAsync(GetProductsQuery query)
        {
            var url = $"{_productUrl}/v1/products?service={query.Service}";

            var request = HttpClientRequest.Get(url);

            var response = await _http.ExecuteAsync<List<Product>>(request);

            if (response != null && response.StatusCode == HttpStatusCode.OK && response.Result != null)
            {
                return response.Result;
            }

            return null;
        }
    }
}
