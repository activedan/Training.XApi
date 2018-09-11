using Training.XApi.Engine.Models.Products;
using Training.XApi.Engine.Settings;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.Exceptions;
using Training.XApi.Infrastructure.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Yokozuna.Logging.Extensions;

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
        ILogger _logger;

        private readonly string _productUrl;

        public GetProductsQueryHandler(IHttpClient http, ILogger<GetProductsQueryHandler> logger, ISettings settings)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<Product>> HandleAsync(GetProductsQuery query)
        {
            try
            {
                var url = $"{_productUrl}/v1/products?service={query.Service}";

                var request = HttpClientRequest.Get(url);

                var response = await _http.ExecuteAsync<List<Product>>(request);

                if (response != null && response.StatusCode == HttpStatusCode.OK && response.Result != null)
                {
                    return response.Result;
                }
                else
                {
                    throw new ApiException(url, response.StatusCode, response.Body, response.IsTimeout, response.WebExceptionStatus,
                        new LogTags().Add(query), $"Unable to get Products data : {query.Service}");
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
