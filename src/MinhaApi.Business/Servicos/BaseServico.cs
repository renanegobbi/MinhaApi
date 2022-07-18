using MinhaApi.Business.Notificacoes;
using MinhaApi.Core.Data;

namespace MinhaApi.Business.Servicos
{
    public abstract class BaseServico : Notificador
    {
        protected readonly IUnitOfWork _uow;

        protected BaseServico(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}