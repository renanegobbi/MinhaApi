using FluentAssertions;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Entidades.Validations;
using Xunit;

namespace MinhaApi.Domain.Tests.Entidades
{
    [Collection(nameof(FornecedorCollection))]
    public class FornecedorTests
    {
        private readonly FornecedorTestsFixture _fornecedorTestsFixture;

        public FornecedorTests(FornecedorTestsFixture forncedorTestsFixture)
        {
            _fornecedorTestsFixture = forncedorTestsFixture;
        }

        [Fact(DisplayName = "Novo fornecedor ativo")]
        [Trait("Categoria", "Fornecedor")]
        public void InstanciarFornecedor_NovoFornecedor_DeveEstarAtivo()
        {
            //Arrange & Act
            var fornecedor = new Fornecedor();

            //Assert
            fornecedor.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Novo Fornecedor Válido")]
        [Trait("Categoria", "Fornecedor Fixture Testes")]
        public void Fornecedor_NovoFornecedor_DeveEstarValido()
        {
            // Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();

            // Act
            var result = new FornecedorValidation().Validate(fornecedor);

            // Assert 
            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Equals(0);
        }

        [Fact(DisplayName = "Novo Fornecedor Inválido")]
        [Trait("Categoria", "Fornecedor Fixture Testes")]
        public void Fornecedor_NovoFornecedor_DeveEstarInvalido()
        {
            // Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();

            // Act
            var result = new FornecedorValidation().Validate(fornecedor);

            // Assert 
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
