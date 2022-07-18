using Swashbuckle.AspNetCore.Filters;
using System;
using MinhaApi.Api.ViewModels.Produto;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Enums;
using MinhaApi.Business.Resources;

namespace MinhaApi.Api.Swagger.Exemplos
{
    //Requisição
    public class ConsultarProdutoRequestExemplo : IExamplesProvider<ConsultarProdutoViewModel>
    {
        public ConsultarProdutoViewModel GetExamples() => new ConsultarProdutoViewModel
        {
            Id = 1,
            FornecedorId = 1,
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now.AddDays(10),
            Ativo = true,
            OrdenarPor = ProdutoOrdenarPor.Id,
            OrdenarSentido = "ASC",
            PaginaIndex = 1,
            PaginaTamanho = 5
        };
    }

    //Obter por id
    public class ObterProdutoSaidaResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { ProdutoResource.Produto_Obtido_Com_Sucesso },
            Retorno = new[]{
                new ProdutoSaida(){
                    Id = 1,
                    FornecedorId = 1,
                    Descricao = "Descrição do produto....",
                    DataFabricacao= DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(10),
                    Ativo = true
                }
            }
        };
    }

    //Consulta
    public class ProdutoExemplo : ProdutoSaida
    {
        public ProdutoSaida ProdutoExemploConsulta() => new ProdutoSaida()
        {
            Id = 1,
            FornecedorId = 1,
            Descricao = "Descrição do produto....",
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now.AddDays(10),
            Ativo = true
        };
    }

    public class ConsultarProdutoExemplo : ConsultarSaida
    {
        public ConsultarProdutoExemplo()
           : base(new ProdutoSaida[] { new ProdutoExemplo { }.ProdutoExemploConsulta() }, ProdutoOrdenarPor.Id.ToString(), "DESC", 1, 1, 5) { }
    }

    public class ConsultarProdutoResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { ProdutoResource.Produto_Consulta_Realizada_Com_Sucesso },
            Retorno = new ConsultarProdutoExemplo().Retorno
        };
    }

    //Cadastrar
    public class CadastrarProdutoRequestExemplo : IExamplesProvider<CadastrarProdutoViewModel>
    {
        public CadastrarProdutoViewModel GetExamples() => new CadastrarProdutoViewModel
        {
            FornecedorId = 1,
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now.AddDays(10),
            Descricao = "Descrição do produto..."
        };
    }

    public class ProdutoSaidaExemplo : ProdutoSaida
    {
        public ProdutoSaida ProdutoExemploCadastro() => new ProdutoSaida()
        {
            Id = 1,
            FornecedorId = 1,
            Descricao = "Descrição do produto....",
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now.AddDays(10),
            Ativo = true
        };
    }

    public class CadastrarProdutoResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { ProdutoResource.Produto_Cadastrado_Com_Sucesso },
            Retorno = new ProdutoSaidaExemplo().ProdutoExemploCadastro()
        };
    }

    //Alterar
    public class AlterarProdutoRequestExemplo : IExamplesProvider<AlterarProdutoViewModel>
    {
        public AlterarProdutoViewModel GetExamples() => new AlterarProdutoViewModel
        {
            Id = 100,
            FornecedorId = 1,
            DataFabricacao = DateTime.Now,
            DataValidade = DateTime.Now.AddDays(10),
            Descricao = "Nova descrição",
            Ativo = true
        };
    }

    public class AlterarProdutoResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { ProdutoResource.Produto_Alterado_Com_Sucesso },
            Retorno = new[]{
                new ProdutoSaida(){
                    Id = 1,
                    FornecedorId = 1,
                    Descricao = "Descrição do produto....",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(10),
                    Ativo = true
                }
            }
        };
    }

    //Desativar
    public class DesativarProdutoResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { ProdutoResource.Produto_Excluido_Com_Sucesso },
            Retorno = new[]{
                new ProdutoSaida(){
                    Id = 1,
                    FornecedorId = 1,
                    Descricao = "Descrição do produto....",
                    DataFabricacao = DateTime.Now,
                    DataValidade = DateTime.Now.AddDays(10),
                    Ativo = false
                }
            }
        };
    }
}
