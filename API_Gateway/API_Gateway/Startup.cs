    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services;

namespace API_Gateway
{
    public class Startup
    {
        const string SWAGGER_SECTION_SETTING_KEY = "SwaggerSettings";
        const string SWAGGER_SECTION_SETTING_TITLE_KEY = "Title";
        const string SWAGGER_SECTION_SETTING_VERSION_KEY = "Version";
        public Startup(IWebHostEnvironment env)
        {
            // "appsettings." + env.EnvironmentName + ".json"
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddTransient<IPricingBookBs, PricingBooksBs>();
            services.AddTransient<IClientsBackingService, ClientsBackingService>();
            services.AddTransient<IQuoteBackingService, QuoteBackingService>();
            services.AddTransient<IClientsBackingService, ClientsBackingService>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.WithOrigins("*")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      );

            });

            var swaggerTitle = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_TITLE_KEY);
            var swaggerVersion = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_VERSION_KEY);

            // COPY THIS TO ENABLE SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc
                (
                    swaggerVersion.Value,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = swaggerTitle.Value,
                        Version = swaggerVersion.Value
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var swaggerTitle = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_TITLE_KEY);
            var swaggerVersion = Configuration
                .GetSection(SWAGGER_SECTION_SETTING_KEY)
                .GetSection(SWAGGER_SECTION_SETTING_VERSION_KEY); app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // COPY THIS TO ENABLE SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerVersion.Value}/swagger.json", swaggerTitle.Value);
            });
        }
    }
}
