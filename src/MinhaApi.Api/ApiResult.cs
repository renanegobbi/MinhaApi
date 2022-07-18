using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MinhaApi.Business.Interfaces.Comandos.Saida;

namespace MinhaApi.Api
{
    /// <summary>
    /// Resultado padrão para todas as rotas da API
    /// </summary>
    public class ApiResult : IActionResult
    {
        private readonly ISaida _saida;

        public ApiResult(ISaida saida)
        {
            _saida = saida;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var jsonResult = new JsonResult(_saida)
            {
                StatusCode = !_saida.Sucesso
                    ? (int)HttpStatusCode.BadRequest
                    : (int)HttpStatusCode.OK
            };

            await jsonResult.ExecuteResultAsync(context);
        }
    }
}
