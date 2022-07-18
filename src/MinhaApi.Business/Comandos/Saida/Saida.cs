using System.Collections.Generic;
using MinhaApi.Business.Interfaces.Comandos.Saida;

namespace MinhaApi.Business.Comandos.Saida
{
    public class Saida : ISaida
    {
        public bool Sucesso { get; }

        public IEnumerable<string> Mensagens { get; }

        public object Retorno { get; }

        public Saida(bool sucesso, IEnumerable<string> mensagens, object retorno)
        {
            Sucesso = sucesso;
            Mensagens = mensagens;
            Retorno = retorno;
        }
    }
}
