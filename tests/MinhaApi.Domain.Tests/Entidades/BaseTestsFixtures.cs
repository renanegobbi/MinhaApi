using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using Moq.AutoMock;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Business.Servicos;

namespace MinhaApi.Domain.Tests.Entidades
{
    public class BaseTestsFixtures
    {
        public ProdutoServico produtoServico;
        public FornecedorServico FornecedorServico;
        public AutoMocker Mocker;

        public Fornecedor GerarFornecedorValido()
        {
            return GerarFornecedores(1, true).FirstOrDefault();
        }

        public IEnumerable<Fornecedor> GerarFornecedores(int quantidade, bool ativo)
        {
            var fornecedores = new Faker<Fornecedor>("pt_BR")
               .CustomInstantiator(f => new Fornecedor()
               {
                   Id = f.Random.Number(1, 100),
                   Nome = f.Company.CompanyName(),
                   Descricao = f.Lorem.Text(),
                   Cnpj = f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", ""),
                   Ativo = ativo
               });

            return fornecedores.Generate(quantidade);
        }

        public string ExibirMensagens(IEnumerable<string> mensagens)
        {
            return $"Mensagem: {string.Join(" ", new List<string>(mensagens).ToArray())}";
        }

        public void Configurar_FornecedorRepositorio_ObterPorId_RetornaFornecedor(Fornecedor fornecedor)
        {
            this.Mocker
                .GetMock<IFornecedorRepositorio>().Setup(f => f.ObterPorId(It.IsAny<int>()))
                .Returns(Task.FromResult(fornecedor));
        }

        public void Configurar_FornecedorRepositorio_ObterPorId_RetornarNulo()
        {
            this.Mocker
                .GetMock<IFornecedorRepositorio>().Setup(f => f.ObterPorId(It.IsAny<int>()))
                .Returns(Task.FromResult((Fornecedor)null));
        }
    }
}
