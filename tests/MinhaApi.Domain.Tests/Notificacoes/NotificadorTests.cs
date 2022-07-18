using MinhaApi.Business.Notificacoes;
using Xunit;

namespace MinhaApi.Domain.Tests.Notificacoes
{
    public class ClasseNotificavel : Notificador
    {
        public string Propriedade1 { get; set; }

        public string Propriedade2 { get; set; }
    }

    public class NotificadorTests: Notificador
    {
        [Fact(DisplayName = "Adiciona uma notificacao")]
        [Trait("Categoria", "Notificacoes")]
        public void Notificacoes_AdicionarNotificacao_DeveAdicionarNovaNotificacao()
        {
            var obj = new ClasseNotificavel();

            obj.AdicionarNotificacao("Notificação 1");
            obj.AdicionarNotificacao("Notificação 2");

            Assert.True(obj.Notificacoes.Count == 2);
        }
    }
}
