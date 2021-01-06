using Experimental.System.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
