using Training.XApi.Engine.Models.Products;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Exceptions;
using Training.XApi.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Yokozuna.Logging.Extensions;

namespace Training.XApi.Engine.Handlers.Queries.Products
{
    public class ProductByIdQuery
    {
        public Guid ProductId    { get; private set; }

        public ProductByIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }    

    internal class ProductByIdQueryHandler : IQueryHandlerAsync<ProductByIdQuery, Product>
    {
        private ILogger _logger;
        private IHttpClient _httpClient;

        private readonly string _productUrl;

        public ProductByIdQueryHandler(ILogger<ProductByIdQueryHandler> logger, IHttpClient httpClient, ISettings settings)
        {
            _logger = logger;
            _httpClient = httpClient;
            _productUrl = settings.ProductApiUrl;
        }

        public async Task<Product> HandleAsync(ProductByIdQuery query)
        {
            try
            {
                var url = $"{_productUrl}/v1/products/{query.ProductId}";

                HttpClientRequest request = HttpClientRequest.Get(url);

                var response = _httpClient.Execute<Product>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return response.Result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw new ApiException(url, response.StatusCode, response.Body, response.IsTimeout, response.WebExceptionStatus, query.ProductId, "Unable to get product by id");
            }
            catch (ApiException exception)
            {
                _logger.Error(exception, exception.LogTags, exception.Message);
                return null;
            }
            catch (Exception exception)
            {
                _logger.Error(exception, new LogTags().Add(query.ProductId), "Unable to get product by id");
                return null;
            }
        }
    }
}
