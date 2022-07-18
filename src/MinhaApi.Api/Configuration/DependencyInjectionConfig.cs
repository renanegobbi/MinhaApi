using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Business.Interfaces.Servicos;
using MinhaApi.Business.Notificacoes;
using MinhaApi.Business.Servicos;
using MinhaApi.Core.Data;
using MinhaApi.Data;
using MinhaApi.Data.Context;
using MinhaApi.Data.Repositorios;

namespace MinhaApi.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MinhaApiContext>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IFornecedorRepositorio, FornecedorRepositorio>();

            services.AddScoped<IProdutoServico, ProdutoServico>();
            services.AddScoped<IFornecedorServico, FornecedorServico>();

            services.AddScoped<INotificador, Notificador>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}