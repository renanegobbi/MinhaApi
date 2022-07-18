using MinhaApi.Business.Notificacoes;
using MinhaApi.Business.Util.Validacoes;
using Xunit;

namespace MinhaApi.Domain.Tests.Util
{
    public class ObjExemplo
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }

    public class ValidacoesObjectTests: Notificador
    {
        [Fact(DisplayName = "Notifica caso objeto seja nulo")]
        [Trait("Categoria", "Object")]
        public void Object_NotificarSeNulo_DeveNotificarSeNulo()
        {
            //Arrange
            ObjExemplo obj = null;

            //Act
            this.NotificarSeNulo(obj, "O objeto é nulo.");

            //Assert
            Assert.True(this.Invalido);
        }
    }
}
