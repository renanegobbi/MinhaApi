using FluentAssertions;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Entidades.Validations;
using Xunit;

namespace MinhaApi.Domain.Tests.Entidades
{
    [Collection(nameof(ProdutoCollection))]
    public class ProdutoTests
    {
        private readonly ProdutoTestsFixture _produtoTestsFixture;

        public ProdutoTests(ProdutoTestsFixture produtoTestsFixture)
        {
            _produtoTestsFixture = produtoTestsFixture;
        }

        [Fact(DisplayName = "Novo produto ativo")]
        [Trait("Categoria", "Produto")]
        public void InstanciarProduto_NovoProduto_DeveEstarAtivo()
        {
            //Arrange & Act
            var produto = new Produto();

            //Assert
            produto.Ativo.Should().BeTrue();
        }

        [Fact(DisplayName = "Novo Produto Válido")]
        [Trait("Categoria", "Produto Fixture Testes")]
        public void Produto_NovoProduto_DeveEstarValido()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();

            // Act
            var result = new ProdutoValidation().Validate(produto);

            // Assert 
            result.IsValid.Should().BeTrue();
            result.Errors.Count.Should().Equals(0);
        }

        [Fact(DisplayName = "Novo Produto Inválido")]
        [Trait("Categoria", "Produto Fixture Testes")]
        public void Produto_NovoProduto_DeveEstarInvalido()
        {
            // Arrange
            var produto = _produtoTestsFixture.GerarProdutoInvalido();
            
            // Act
            var result = new ProdutoValidation().Validate(produto);

            // Assert 
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
