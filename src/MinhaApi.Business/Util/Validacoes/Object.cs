using System.Collections.Generic;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Notificacoes;

namespace MinhaApi.Business.Util.Validacoes
{
    public static partial class Notificar
    {
        /// Adiciona uma notificação caso o objeto seja nulo
        public static INotificador NotificarSeNulo(this INotificador notificavel, object objeto, string mensagem, Dictionary<string, string> informacoesAdicionais = null)
        {
            if (notificavel == null)
                return null;

            if (objeto == null)
                notificavel.Handle(new Notificacao(mensagem));

            return notificavel;
        }
    }
}
