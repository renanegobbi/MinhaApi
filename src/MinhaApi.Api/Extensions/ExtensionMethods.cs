using Microsoft.Extensions.Hosting;

namespace MinhaApi.Api.Extensions
{
    public static partial class ExtensionMethods
    {
        public static string ObterDescricao(this IHostEnvironment environment)
        {
            switch (environment.EnvironmentName)
            {
                case "Desenvolvimento":
                    return "Desenvolvimento";
                case "Homologacao":
                    return "Homologação";
                case "Producao":
                    return "Produção";
                default: return string.Empty;
            }
        }

        public static string ObterSigla(this IHostEnvironment environment)
        {
            switch (environment.EnvironmentName)
            {
                case "Development":
                    return "DES";
                case "Homologacao":
                    return "HOM";
                case "Producao":
                    return "PRD";
                default: return string.Empty;
            }
        }
    }
}
