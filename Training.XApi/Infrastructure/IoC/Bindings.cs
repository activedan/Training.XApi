using Training.XApi.Infrastructure.CQRS;
using Microsoft.Extensions.DependencyInjection;
using Training.XApi.Infrastructure.Http;
using Training.XApi.Infrastructure.Serialisers;
using System;
using Microsoft.Extensions.Configuration;

namespace Training.XApi.Infrastructure.IoC
{
    public static class Bindings
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.AddSingleton<IHttpClient, HttpClient>();
            services.AddSingleton<IHttpRequestSerialiser, JsonHttpSerialiser>();
            services.AddSingleton<IHttpResponseSerialiser, JsonHttpSerialiser>();

            return services;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static TConfig ConfigureSettings<TInterface, TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, TInterface, new() where TInterface : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();
            configuration.Bind(config);
            services.AddSingleton<TInterface>(config);
            return config;
        }
    }
}
