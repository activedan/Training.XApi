using Training.XApi.Engine.Models.Products;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Http;
using System;
using System.Threading.Tasks;

namespace Training.XApi.Engine.Handlers.Queries.Products
{
    public class ProductByIdQuery
    {
        public Guid ProductId { get; private set; }

        public ProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }

    internal class ProductByIdQueryHandler : IQueryHandlerAsync<ProductByIdQuery, Product>
    {
        private IHttpClient _httpClient;

        private readonly string _productUrl;

        public ProductByIdQueryHandler(IHttpClient httpClient, ISettings settings)
        {
            _httpClient = httpClient;
            _productUrl = settings.ProductApiUrl;
        }

        public async Task<Product> HandleAsync(ProductByIdQuery query)
        {
            var url = $"{_productUrl}/v1/products/{query.ProductId}";

            HttpClientRequest request = HttpClientRequest.Get(url);

            var response = _httpClient.Execute<Product>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Result;
            }

            return null;
        }
    }
}
