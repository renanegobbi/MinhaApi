using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MinhaApi.Api.Extensions;

namespace MinhaApi.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggeConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.EnableAnnotations();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.ExampleFilters();
                options.IncludeXmlComments(xmlPath);
                options.EnableAnnotations();
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            return services;
        }

        public class GetStreamManifestResource
        {
            public Stream ManifestResourceStream() => GetType().GetTypeInfo().GetTypeInfo()
                .Assembly.GetManifestResourceStream("MinhaApi.Api.Swagger.UI.Index.html");
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, 
            IApiVersionDescriptionProvider provider, IWebHostEnvironment webHostEnvironment)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "docs"; // Define a documentação no endereço http://{url}/docs/
                options.DocumentTitle = "API MinhaApi - Documentação";
                options.DefaultModelsExpandDepth(-1); // Oculta a sessão "Models"
                options.DocExpansion(DocExpansion.None);

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/api/minhaapi/docs/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                options.InjectStylesheet("/swagger-ui/custom.css");
                options.InjectStylesheet($"/swagger-ui/custom-{webHostEnvironment.EnvironmentName}.css");

                options.IndexStream = () => new GetStreamManifestResource().ManifestResourceStream();

                options.EnableValidator(null);
            });

            return app;
        }
    }

    public static class EnvironmentNames
    {
        public const string Development = "Development";
        public const string DbDevelopmentName = "MinhaApi";

        public const string Staging = "Staging";
        public const string DbStagingName = "HMinhaApi";

        public const string Production = "Production";
        public const string DbProductionName = "PMinhaApi";
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        readonly IWebHostEnvironment webHostEnvironment;

        readonly IConfiguration configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.provider = provider;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, webHostEnvironment, this.configuration));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description,
           IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            var nomeBancoDados = string.Empty;
            var ambiente = string.Empty;

            var environmentName = webHostEnvironment.EnvironmentName;

            switch (environmentName)
            {
                case EnvironmentNames.Development:
                    nomeBancoDados = EnvironmentNames.DbDevelopmentName;
                    break;
                case EnvironmentNames.Staging:
                    nomeBancoDados = EnvironmentNames.DbStagingName;
                    break;
                case EnvironmentNames.Production:
                    nomeBancoDados = EnvironmentNames.DbProductionName;
                    break;
            }

            string urlApi = new ApiProxy(webHostEnvironment.ObterSigla()).ObterUrlApi(); 

            var customDescription = "API que disponibiliza a gestão de produtos da MinhaApi.<br>" +
                                            "<ul>" +
                                                $"<li>Ambiente atual: <b>{environmentName.ToUpper()}</b>.</li>" +
                                                $"<li>Banco de dados (SQLite): <b>{nomeBancoDados}</b>.</li>" +
                                                $"<li>URL da API: <b>{urlApi}</b></li>" +
                                            "</ul>";

            customDescription += "<p>Informações de contato:</p>" +
                           "<ul>" +
                                "<li>E-mail: <b>renan@email.com</b></li>" +
                           "</ul>";

            var info = new OpenApiInfo()
            {
                Title = "MinhaApi",
                Version = description.ApiVersion.ToString(),
                Description = customDescription,
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                {
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}


