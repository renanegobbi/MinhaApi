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
    [Collection(nameof(FornecedorCollection))]
    public class FornecedorServicoTests
    {
        private readonly FornecedorTestsFixture _fornecedorTestsFixture;
        private readonly FornecedorServico _fornecedorServico;
        readonly ITestOutputHelper _outputHelper;

        public FornecedorServicoTests(FornecedorTestsFixture forncedorTestsFixture, ITestOutputHelper outputHelper)
        {
            _fornecedorTestsFixture = forncedorTestsFixture;
            _fornecedorServico = _fornecedorTestsFixture.ObterFornecedorServico();
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "Obter Cnpj com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public void FornecedorService_ObterCnpj_DeveExecutarComSucesso()
        {
            //Arrange
            var cnpjValido = _fornecedorTestsFixture.GerarCnpjValido();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorCnpj_RetornarVerdadeiro();

            // Act
            var result = _fornecedorServico.ObterFornecedorPorCnpj(cnpjValido).Result;

            // Assert 
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Obter Cnpj sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public void FornecedorService_ObterCnpj_DeveExecutarSemSucesso()
        {
            //Arrange
            var cnpjValido = _fornecedorTestsFixture.GerarCnpjValido();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorCnpj_RetornarFalso();

            // Act
            var result = _fornecedorServico.ObterFornecedorPorCnpj(cnpjValido).Result;

            // Assert 
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Obter Fornecedor por Id com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public void FornecedorService_ObterPorId_DeveExecutarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);

            // Act
            var result = _fornecedorServico.ObterFornecedorPorId(fornecedor.Id);

            // Assert 
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "Obter Fornecedor por Id sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public void FornecedorService_ObterPorId_DeveExecutarSemSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornarNulo();
                
            // Act
            var result = _fornecedorServico.ObterFornecedorPorId(fornecedor.Id).Result;

            // Assert 
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Obter Fornecedor com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_ObterFornecedor_DeveExecutarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();

            // Act
            await _fornecedorServico.ObterFornecedor(fornecedor.Id);

            // Assert 
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.ObterPorId(fornecedor.Id), Times.Once);
        }

        [Fact(DisplayName = "Obter Todos Fornecedores com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_ObterTodos_DeveExecutarComSucesso()
        {
            //Arrange
            var entrada = new ProcurarFornecedorEntrada();
            var fornecedores = _fornecedorTestsFixture.ObterFornecedoresVariados();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterTodos(fornecedores);

            // Act
            var result = await _fornecedorServico.ObterTodos(entrada);

            // Assert 
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.ObterTodos(entrada), Times.Once);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Obter Todos Fornecedores sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_ObterTodos_DeveExecutarSemSucesso()
        {
            //Arrange
            var entrada = new ProcurarFornecedorEntrada();
            var fornecedores = _fornecedorTestsFixture.ObterFornecedoresVariados();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterTodos(fornecedores);

            // Act
            var result = await _fornecedorServico.ObterTodos(null);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.ObterTodos(entrada), Times.Never);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Adicionar Fornecedor com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_AdicionarFornecedor_DeveExecutarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            var fornecedorSaida = _fornecedorTestsFixture.GerarFornecedorSaida();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorCnpj_RetornarFalso();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);          
            _fornecedorTestsFixture.Configurar_AutoMapper_DeFornecedor_ParaFornecedorSaida(fornecedorSaida);

            // Act
            var result = await _fornecedorServico.AdicionarFornecedor(fornecedor);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.Adicionar(fornecedor), Times.Once);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Adicionar Fornecedor sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_AdicionarFornecedor_DeveExecutarSemSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();

            // Act
            var result = await _fornecedorServico.AdicionarFornecedor(fornecedor);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.Adicionar(fornecedor), Times.Never);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Alterar Fornecedor com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_AlterarFornecedor_DeveExecutarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            var fornecedorSaida = _fornecedorTestsFixture.GerarFornecedorSaida();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);
            _fornecedorTestsFixture.Configurar_AutoMapper_DeFornecedor_ParaFornecedorSaida(fornecedorSaida);

            // Act
            var result = await _fornecedorServico.AlterarFornecedor(fornecedor);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.Atualizar(fornecedor), Times.Once);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Alterar Fornecedor sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_AlterarFornecedor_DeveExecutarSemSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorInvalido();
            var fornecedorSaida = _fornecedorTestsFixture.GerarFornecedorSaida();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);
            _fornecedorTestsFixture.Configurar_AutoMapper_DeFornecedor_ParaFornecedorSaida(fornecedorSaida);

            // Act
            var result = await _fornecedorServico.AlterarFornecedor(fornecedor);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.Atualizar(fornecedor), Times.Never);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Desativar Fornecedor com Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_DesativarFornecedor_DeveExecutarComSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            var fornecedorSaida = _fornecedorTestsFixture.GerarFornecedorSaida();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(fornecedor);
            _fornecedorTestsFixture.Configurar_AutoMapper_DeFornecedor_ParaFornecedorSaida(fornecedorSaida);
            _fornecedorTestsFixture.Configurar_AutoMapper_DeFornecedorSaida_ParaFornecedor(fornecedor);

            // Act
            var result = await _fornecedorServico.DesativarFornecedor(fornecedor.Id);

            // Assert 
            result.Sucesso.Should().BeTrue();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.DesativarFornecedor(fornecedor), Times.Once);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }

        [Fact(DisplayName = "Desativar Fornecedor sem Sucesso")]
        [Trait("Categoria", "Fornecedor Servico Mock Tests")]
        public async Task FornecedorService_DesativarFornecedor_DeveExecutarSemSucesso()
        {
            //Arrange
            var fornecedor = _fornecedorTestsFixture.GerarFornecedorValido();
            var fornecedorSaida = _fornecedorTestsFixture.GerarFornecedorSaida();
            _fornecedorTestsFixture.Configurar_FornecedorRepositorio_ObterPorId_RetornarNulo();

            // Act
            var result = await _fornecedorServico.DesativarFornecedor(fornecedor.Id);

            // Assert 
            result.Sucesso.Should().BeFalse();
            _fornecedorTestsFixture.Mocker.GetMock<IFornecedorRepositorio>().Verify(f => f.DesativarFornecedor(fornecedor), Times.Never);
            _outputHelper.WriteLine(_fornecedorTestsFixture.ExibirMensagens(result.Mensagens));
        }
    }
}