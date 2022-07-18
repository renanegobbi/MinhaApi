using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Notificacoes;

namespace MinhaApi.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificador _notificador;
        protected readonly ILogger _logger;

        protected BaseController(INotificador notificador, ILogger logger = null)
        {
            _notificador = notificador;
            _logger = logger;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null, List<string> mensagens = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    sucesso = true,
                    mensagens = mensagens,
                    retorno = result
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                mensagens = _notificador.ObterNotificacoes().Select(n => n.Mensagem),
                retorno = result
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
