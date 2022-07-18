using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
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
    [CollectionDefinition(nameof(FornecedorCollection))]

    public class FornecedorCollection : ICollectionFixture<FornecedorTestsFixture>
    { }

    public class FornecedorTestsFixture : BaseTestsFixtures, IDisposable
    {
        public string GerarCnpjValido()
        {
            return GerarCnpj(1).FirstOrDefault();
        }

        public FornecedorSaida GerarFornecedorSaida()
        {
            return GerarFornecedorSaida(1, true).FirstOrDefault();
        }

        public IEnumerable<Fornecedor> ObterFornecedoresVariados()
        {
            var fornecedores = new List<Fornecedor>();

            fornecedores.AddRange(GerarFornecedores(50, true).ToList());
            fornecedores.AddRange(GerarFornecedores(50, false).ToList());

            return fornecedores;
        }

        public Fornecedor GerarFornecedorInvalido()
        {
            var fornecedor = new Faker<Fornecedor>("pt_BR")
               .CustomInstantiator(f => new Fornecedor()
               {
                   Id = f.Random.Number(1, 100),
                   Nome = f.Random.String2(1),
                   Descricao = f.Lorem.Text(),
                   Cnpj = f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", ""),
                   Ativo = true
               });

            return fornecedor;
        }

        public IEnumerable<FornecedorSaida> GerarFornecedorSaida(int quantidade, bool ativo)
        {
            var fornecedorSaida = new Faker<FornecedorSaida>("pt_BR")
               .CustomInstantiator(f => new FornecedorSaida()
               {
                   Id = f.Random.Number(1, 100),
                   Nome = f.Company.CompanyName(),
                   Descricao = f.Lorem.Text(),
                   Cnpj = f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", ""),
                   Ativo = ativo
               });

            return fornecedorSaida.Generate(quantidade);
        }

        public IEnumerable<string> GerarCnpj(int quantidade)
        {
            var cnpj = new Faker<string>("pt_BR")
               .CustomInstantiator(f => 
                    f.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", ""));

            return cnpj.Generate(quantidade);
        }
       
        public FornecedorServico ObterFornecedorServico()
        {
            Mocker = new AutoMocker();
            var fornecedorServico = Mocker.CreateInstance<FornecedorServico>();

            return fornecedorServico;
        }

        public void Configurar_FornecedorRepositorio_ObterTodos(IEnumerable<Fornecedor> fornecedores)
        {
            this.Mocker
                .GetMock<IFornecedorRepositorio>().Setup(f => f.ObterTodos(It.IsAny<ProcurarFornecedorEntrada>()))
                .Returns(Task.FromResult(new Tuple<Fornecedor[], double>(fornecedores.ToArray(), fornecedores.Count())));
        }

        public void Configurar_FornecedorRepositorio_ObterPorCnpj_RetornarFalso()
        {
            this.Mocker
                .GetMock<IFornecedorRepositorio>().Setup(f => f.ObterPorCnpj(It.IsAny<string>()))
                .Returns(Task.FromResult(false));
        }

        public void Configurar_FornecedorRepositorio_ObterPorCnpj_RetornarVerdadeiro()
        {
            this.Mocker
                .GetMock<IFornecedorRepositorio>().Setup(f => f.ObterPorCnpj(It.IsAny<string>()))
                .Returns(Task.FromResult(true));
        }

        public void Configurar_AutoMapper_DeFornecedor_ParaFornecedorSaida(FornecedorSaida fonecedorSaida)
        {
            this.Mocker
                .GetMock<IMapper>().Setup(f => f.Map<FornecedorSaida>(It.IsAny<Fornecedor>()))
                .Returns(fonecedorSaida);
        }

        public void Configurar_AutoMapper_DeFornecedorSaida_ParaFornecedor(Fornecedor fornecedor)
        {
            this.Mocker
                .GetMock<IMapper>().Setup(f => f.Map<Fornecedor>(It.IsAny<FornecedorSaida>()))
                .Returns(fornecedor);
        }

        public void Dispose()
        {
        }
    }
}
