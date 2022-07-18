using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinhaApi.Api.Configuration;

namespace MinhaApi.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IHostEnvironment hostEnvironment, IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddApiConfiguration(Configuration);

            services.ResolveDependencies();

            services.AddSwaggeConfiguration();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IApiVersionDescriptionProvider provider, IWebHostEnvironment environment)
        {
            app.UseApiConfiguration(env, Configuration);

            app.UseSwaggerConfiguration(provider, environment);
        }
    }
}
