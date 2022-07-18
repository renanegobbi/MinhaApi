using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Business.Servicos;
using MinhaApi.Domain.Tests.Entidades;
using Xunit;
using Xunit.Abstractions;

namespace MinhaApi.Domain.Tests.Services
{
    [Collection(nameof(ProdutoCollection))]
    public class ProdutoServicoTests
    {
        private readonly ProdutoTestsFixture _produtoTestsFixture;
        private readonly ProdutoServico _produtoServico;
        readonly ITestOutputHelper _outputHelper;

        public ProdutoServicoTests(ProdutoTestsFixture produtoTestsFixture, ITestOutputHelper outputHelper)
        {
            _produtoTestsFixture = produtoTestsFixture;
            _produtoServico = _produtoTestsFixture.ObterProdutoServico();
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "Obter Produto por Id com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public void ProdutoService_ObterPorId_DeveExecutarComSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarProduto(produto);

            // Act
            var result = _produtoServico.ObterProdutoPorId(produto.Id);

            // Assert 
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Obter Produto por Id sem Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public void ProdutoService_ObterPorId_DeveExecutarSemSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarNulo();

            // Act
            var result = _produtoServico.ObterProdutoPorId(produto.Id).Result;

            // Assert 
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Obter Produto com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_ObterProduto_DeveExecutarComSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();

            // Act
            var result = await _produtoServico.ObterProduto(produto.Id);

            // Assert 
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.ObterPorId(produto.Id), Times.Once);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Obter Todos Produtos com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_ObterTodos_DeveExecutarComSucesso()
        {
            //Arrange
            var entrada = new ProcurarProdutoEntrada();
            var produtos = _produtoTestsFixture.ObterProdutosVariados();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterTodos(produtos);

            // Act
            var result = await _produtoServico.ObterTodos(entrada);

            // Assert 
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.ObterTodos(entrada), Times.Once);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Obter Todos Produtos sem Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_ObterTodos_DeveExecutarSemSucesso()
        {
            //Arrange
            var entrada = new ProcurarProdutoEntrada();
            var produtos = _produtoTestsFixture.ObterProdutosVariados();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterTodos(produtos);

            // Act
            var result = await _produtoServico.ObterTodos(null);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.ObterTodos(entrada), Times.Never);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Adicionar Produto com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_AdicionarProduto_DeveExecutarComSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var fornecedor = _produtoTestsFixture.GerarFornecedorValido();
            var produtoSaida = _produtoTestsFixture.GerarProdutoSaida();
            _produtoTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarProduto(produto);
            _produtoTestsFixture.Configurar_AutoMapper_DeProduto_ParaProdutoSaida(produtoSaida);

            // Act
            var result = await _produtoServico.AdicionarProduto(produto);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.Adicionar(produto), Times.Once);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Adicionar Produto sem Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_AdicionarProduto_DeveExecutarSemSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoInvalido();

            // Act
            var result = await _produtoServico.AdicionarProduto(produto);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.Adicionar(produto), Times.Never);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Alterar Produto com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_AlterarProduto_DeveExecutarComSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var fornecedor = _produtoTestsFixture.GerarFornecedorValido();
            var produtoSaida = _produtoTestsFixture.GerarProdutoSaida();
            _produtoTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarProduto(produto);
            _produtoTestsFixture.Configurar_AutoMapper_DeProduto_ParaProdutoSaida(produtoSaida);

            // Act
            var result = await _produtoServico.AlterarProduto(produto);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.Atualizar(produto), Times.Once);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Alterar Produto sem Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_AlterarProduto_DeveExecutarSemSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoInvalido();
            var fornecedor = _produtoTestsFixture.GerarFornecedorValido();
            _produtoTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornarNulo();

            // Act
            var result = await _produtoServico.AlterarProduto(produto);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.Atualizar(produto), Times.Never);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Desativar Produto com Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_DesativarProduto_DeveExecutarComSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            var produtoSaida = _produtoTestsFixture.GerarProdutoSaida();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarProduto(produto);
            _produtoTestsFixture.Configurar_AutoMapper_DeProduto_ParaProdutoSaida(produtoSaida);
            _produtoTestsFixture.Configurar_AutoMapper_DeProdutoSaida_ParaProduto(produto);

            // Act
            var result = await _produtoServico.DesativarProduto(produto.Id);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.DesativarProduto(produto), Times.Once);
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Desativar Produto sem Sucesso")]
        [Trait("Categoria", "Produto Servico Mock Tests")]
        public async Task ProdutoService_DesativarProduto_DeveExecutarSemSucesso()
        {
            //Arrange
            var produto = _produtoTestsFixture.GerarProdutoValido();
            _produtoTestsFixture.Configurar_ProdutoRepositorio_ObterPorId_RetornarNulo();

            // Act
            var result = await _produtoServico.DesativarProduto(produto.Id);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _produtoTestsFixture.Mocker.GetMock<IProdutoRepositorio>().Verify(f => f.DesativarProduto(produto), Times.Never);        
            _outputHelper.WriteLine(_produtoTestsFixture.ExibirMensagens(result.Mensagens));
        }
    }
}