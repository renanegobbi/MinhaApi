using System.Collections.Generic;

namespace MinhaApi.Business.Interfaces.Comandos.Saida
{
    public interface ISaida
    {
        /// <summary>
        /// Indica se houve sucesso
        /// </summary>
        bool Sucesso { get; }

        /// <summary>
        /// Mensagens retornadas
        /// </summary>
        IEnumerable<string> Mensagens { get; }

        /// <summary>
        /// Objeto retornado
        /// </summary>
        object Retorno { get; }
    }
}