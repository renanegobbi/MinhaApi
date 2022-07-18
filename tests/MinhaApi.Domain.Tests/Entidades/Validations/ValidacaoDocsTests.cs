using FluentAssertions;
using MinhaApi.Core.Validations.Documentos;
using Xunit;

namespace MinhaApi.Domain.Tests.Entidades.Validations
{
    public class ValidacaoDocsTests
    {
        [Theory(DisplayName = "Cnpj Validos")]
        [Trait("Categoria", "Cnpj Validacao")]
        [InlineData("16473137000101")]
        [InlineData("17523977000196")]
        [InlineData("13071764000110")]
        [InlineData("88537637000162")]
        [InlineData("37274336000152")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {
            // Assert, Act & Assert
            CnpjValidacao.Validar(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "Cnpj Invalidos")]
        [Trait("Categoria", "Cnpj Validacao")]
        [InlineData("15231766607210")]
        [InlineData("52878168208223")]
        [InlineData("35555868512432")]
        [InlineData("36014132822427")]
        [InlineData("72186126500498")]
        [InlineData("23775274811713")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInvalidos(string cpf)
        {
            // Assert, Act & Assert
            CnpjValidacao.Validar(cpf).Should().BeFalse();
        }
    }
}
