using System.Collections.Generic;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Notificacoes;

namespace MinhaApi.Business.Util.Validacoes
{
    public static partial class Notificar
    {
        /// Adiciona uma notificação caso um determinado número seja menor que outro.
        public static INotificador NotificarSeMenorQue(this INotificador notificavel, int numero, int numeroComparado, string mensagem, Dictionary<string, string> informacoesAdicionais = null)
        {
            if (notificavel == null)
                return null;

            if (numero < numeroComparado)
                notificavel.Handle(new Notificacao(mensagem));

            return notificavel;
        }
    }
}

