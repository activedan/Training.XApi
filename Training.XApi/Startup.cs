using Training.XApi.Engine.IoC;
using Training.XApi.Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Yokozuna.AspNetCore.Logging.NLog.Extensions;

namespace Training.XApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.RegisterInfrastructure(Configuration);
            services.RegisterEngine(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Training Api", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                env.ConfigureNLog("NLog.config");

                app.UseDeveloperExceptionPage();
            }
            else
            {
                env.ConfigureNLog($"NLog.{env.EnvironmentName}.config");

                app.UseExceptionHandler("/error");
            }

            loggerFactory.UseYokozuna();

            app.AddYokozuna();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training Api");
                c.ShowRequestHeaders();
            });
        }

        public static string HostingEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    }
}