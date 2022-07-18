using System.Collections.Generic;
using MinhaApi.Business.Notificacoes;

namespace MinhaApi.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}