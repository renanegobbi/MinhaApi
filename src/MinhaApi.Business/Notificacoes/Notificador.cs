using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using MinhaApi.Business.Interfaces;
using MinhaApi.Core.DomainObjects;

namespace MinhaApi.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        /// Coleção de notificações adicionadas
        [JsonIgnore]
        public IReadOnlyCollection<Notificacao> Notificacoes => _notificacoes;

        /// Coleção de todas as mensagens geradas pelas notificações.
        [JsonIgnore]
        public IReadOnlyCollection<string> Mensagens => _notificacoes.Any() ? _notificacoes.Select(x => x.Mensagem).ToList() : new List<string>();

        /// Indica a existência de pelo menos uma notificação. Havendo uma notificação, é considerado inválido.
        [JsonIgnore]
        public bool Invalido => _notificacoes.Any();

        /// Adiciona uma coleção de notificações
        public void AdicionarNotificacoes(IReadOnlyCollection<Notificacao> notificacoes)
        {
            if (notificacoes != null && notificacoes.Any())
                _notificacoes.AddRange(notificacoes);
        }

        /// Adiciona uma notificação
        public void AdicionarNotificacao(string mensagem)
        {
            _notificacoes.Add(new Notificacao(mensagem));
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }

        protected void NotificarErrorValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            var erros = validator.Errors;

            foreach (var erro in erros)
            {
                AdicionarNotificacao(erro.ErrorMessage);
            }
        }
    }
}
