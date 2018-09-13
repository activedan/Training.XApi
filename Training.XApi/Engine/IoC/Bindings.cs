using Training.XApi.Engine.Handlers.Queries.Products;
using Training.XApi.Engine.Models.Products;
using Training.XApi.Infrastructure.CQRS;
using Training.XApi.Infrastructure.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Training.XApi.Engine.Settings;
using Training.XApi.Engine.Models.Adverts;
using Training.XApi.Engine.Handlers.Queries.Adverts;
using Training.XApi.Engine.Handlers.Queries.Members;
using Training.XApi.Engine.Models.Members;
using Training.XApi.UiFactories.Desktop;

namespace Training.XApi.Engine.IoC
{
    public static class Bindings
    {
        public static IServiceCollection RegisterEngine(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<AdvertListUiFactory>();

            services.AddSingleton<IQueryHandlerAsync<AdvertByIdQuery, Advert>, AdvertByIdQueryHandler>();
            services.AddSingleton<IQueryHandlerAsync<AdvertsByMemberIdQuery, IEnumerable<Advert>>, AdvertsByMemberIdQueryHandler>();

            services.AddSingleton<IQueryHandlerAsync<GetMemberQuery, MemberProfile>, GetMemberQueryHandler>();
            
            services.AddSingleton<IQueryHandlerAsync<ProductByIdQuery, Product>, ProductByIdQueryHandler>();
            services.AddSingleton<IQueryHandlerAsync<GetProductsQuery, List<Product>>, GetProductsQueryHandler>();

            services.ConfigureSettings<ISettings, Settings.Settings>(config.GetSection("Settings"));

            return services;
        }
    }
}
