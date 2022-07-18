using MinhaApi.Business.Notificacoes;
using MinhaApi.Business.Util.Validacoes;
using Xunit;

namespace MinhaApi.Domain.Tests.Util
{
    public class ValidacoesIntTests: Notificador
    {
        [Theory(DisplayName = "Notifica caso um determinado número seja menor que outro")]
        [Trait("Categoria", "Int")]
        [InlineData(1, 2)]
        [InlineData(15, 25)]
        [InlineData(18, 19)]
        [InlineData(20, 40)]
        [InlineData(1234, 1235)]
        public void Int_NotificarSeMenorQue_DeveNotificarSeMenorQueOutro(int valor1, int valor2)
        {
            // Arrange & Act
            this.NotificarSeMenorQue(valor1, valor2, "O valor 1 é menor que o valor 2.");

            // Assert
            Assert.True(this.Invalido);
        }
    }
}
