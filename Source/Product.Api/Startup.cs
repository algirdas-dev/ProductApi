using Experimental.System.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Product.Api.AppConfigs;
using Product.DB;
using Product.Domain;
using Product.Domain.IHelpers.IQueueings;
using Product.Domain.IRepositories;
using Product.Domain.IServices;
using Product.Helpers.Connections;
using Product.Helpers.Generators;
using Product.Helpers.Queueings;
using Product.Infrastructure.CachedData;
using Product.Infrastructure.Repositories;
using Product.Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Product.Api
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
            services.AddControllers();
            services.AddMemoryCache();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c => { 
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<ProductContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddTransient<IDatabaseConnectionFactory>(e =>
            {
                return new SqlConnectionFactory(Configuration.GetConnectionString("sqlConnection"));
            });

            services.AddScoped<CodeGeneratorHelper>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IProductCache,ProductCache>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IMsmqHelper, MsmqHelper>()
                .AddScoped<MessageQueue>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            app.ConfigureCustomExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
