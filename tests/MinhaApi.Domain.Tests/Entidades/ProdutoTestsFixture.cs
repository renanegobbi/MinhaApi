using AutoMapper;
using Bogus;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Business.Servicos;
using Xunit;

namespace MinhaApi.Domain.Tests.Entidades
{
    [CollectionDefinition(nameof(ProdutoCollection))]
    public class ProdutoCollection : ICollectionFixture<ProdutoTestsFixture>
    { }

    public class ProdutoTestsFixture : BaseTestsFixtures, IDisposable
    {
        public ProdutoSaida GerarProdutoSaida()
        {
            return GerarProdutoSaida(1, true).FirstOrDefault();
        }

        public Produto GerarProdutoValido()
        {
            var produto = new Faker<Produto>("pt_BR")
               .CustomInstantiator(p => new Produto()
               {
                   Id = p.Random.Int(1, 100),
                   FornecedorId = p.Random.Int(1, 100),
                   Descricao = p.Lorem.Text(),
                   DataFabricacao = DateTime.Now,
                   DataValidade = DateTime.Now.AddDays(10),
                   Ativo = true
               });

            return produto;
        }

        public Produto GerarProdutoInvalido()
        {
            var produto = new Faker<Produto>("pt_BR")
               .CustomInstantiator(p => new Produto()
               {
                   Id = p.Random.Int(1, 100),
                   FornecedorId = p.Random.Int(1, 100),
                   Descricao = p.Lorem.Text(),
                   DataFabricacao = DateTime.Now.AddDays(10),
                   DataValidade = DateTime.Now,
                   Ativo = true
               });

            return produto;
        }

        public IEnumerable<Produto> ObterProdutosVariados()
        {
            var produtos = new List<Produto>();

            produtos.AddRange(GerarProdutos(50, true).ToList());
            produtos.AddRange(GerarProdutos(50, false).ToList());

            return produtos;
        }

        public IEnumerable<Produto> GerarProdutos(int quantidade, bool ativo)
        {
            var produtos = new Faker<Produto>("pt_BR")
               .CustomInstantiator(p => new Produto()
               {
                   Id = p.Random.Number(1, 100),
                   FornecedorId = p.Random.Number(1, 100),
                   Descricao = p.Lorem.Text(),
                   DataFabricacao = DateTime.Now,
                   DataValidade = DateTime.Now.AddDays(p.Random.Number(10,30)),
                   Ativo = ativo
               });

            return produtos.Generate(quantidade);
        }

        public IEnumerable<ProdutoSaida> GerarProdutoSaida(int quantidade, bool ativo)
        {
            var produtoSaida = new Faker<ProdutoSaida>("pt_BR")
               .CustomInstantiator(p => new ProdutoSaida()
               {
                   Id = p.Random.Number(1, 100),
                   FornecedorId = p.Random.Number(1, 100),
                   Descricao = p.Lorem.Text(),
                   DataFabricacao = DateTime.Now,
                   DataValidade = DateTime.Now.AddDays(p.Random.Number(10, 30)),
                   Ativo = ativo
               });

            return produtoSaida.Generate(quantidade);
        }

        public ProdutoServico ObterProdutoServico()
        {
            Mocker = new AutoMocker();
            var produtoServico = Mocker.CreateInstance<ProdutoServico>();

            return produtoServico;
        }

        public void Configurar_ProdutoRepositorio_ObterPorId_RetornarProduto(Produto produto)
        {
            this.Mocker
                .GetMock<IProdutoRepositorio>().Setup(f => f.ObterPorId(It.IsAny<int>()))
                .Returns(Task.FromResult(produto));
        }

        public void Configurar_ProdutoRepositorio_ObterPorId_RetornarNulo()
        {
            this.Mocker
                .GetMock<IProdutoRepositorio>().Setup(f => f.ObterPorId(It.IsAny<int>()))
                .Returns(Task.FromResult((Produto)null));
        }

        public void Configurar_ProdutoRepositorio_ObterTodos(IEnumerable<Produto> produtos)
        {
            this.Mocker
                .GetMock<IProdutoRepositorio>().Setup(f => f.ObterTodos(It.IsAny<ProcurarProdutoEntrada>()))
                .Returns(Task.FromResult(new Tuple<Produto[], double>(produtos.ToArray(), produtos.Count())));
        }

        public void Configurar_AutoMapper_DeProduto_ParaProdutoSaida(ProdutoSaida produtoSaida)
        {
            this.Mocker
                .GetMock<IMapper>().Setup(f => f.Map<ProdutoSaida>(It.IsAny<Produto>()))
                .Returns(produtoSaida);
        }

        public void Configurar_AutoMapper_DeProdutoSaida_ParaProduto(Produto produto)
        {
            this.Mocker
                .GetMock<IMapper>().Setup(f => f.Map<Produto>(It.IsAny<ProdutoSaida>()))
                .Returns(produto);
        }

        public void Dispose()
        {
        }
    }
}
