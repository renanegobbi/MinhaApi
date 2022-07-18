using System.Collections.Generic;
using MinhaApi.Business.Interfaces.Comandos.Saida;

namespace MinhaApi.Api.Swagger
{
    public class Response : ISaida
    {
        /// <summary>
        /// Indica se houve sucesso no request.
        /// </summary>
        public bool Sucesso { get; set; }

        /// <summary>
        /// Mensagens retornadas.
        /// </summary>
        public IEnumerable<string> Mensagens { get; set; }

        /// <summary>
        /// Objeto retornado no atendimento do request
        /// </summary>
        public object Retorno { get; set; }
    }
}
