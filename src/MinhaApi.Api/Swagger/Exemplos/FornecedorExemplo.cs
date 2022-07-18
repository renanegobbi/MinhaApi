using Swashbuckle.AspNetCore.Filters;
using MinhaApi.Api.ViewModels.Fornecedor;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Enums;
using MinhaApi.Business.Resources;

namespace MinhaApi.Api.Swagger.Exemplos
{
    //Requisição
    public class ConsultarFornecedorRequestExemplo : IExamplesProvider<ConsultarFornecedorViewModel>
    {
        public ConsultarFornecedorViewModel GetExamples() => new ConsultarFornecedorViewModel
        {
            Id = 1,
            Nome = "Fornecedor 1",
            Descricao = "Descrição do fornecedor 1",
            Cnpj = "11621132788",
            Ativo = true,
            OrdenarPor = FornecedorOrdenarPor.Id,
            OrdenarSentido = "ASC",
            PaginaIndex = 1,
            PaginaTamanho = 5
        };
    }

    //Obter por id
    public class ObterFornecedorSaidaResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { FornecedorResource.Fornecedor_Obtido_Com_Sucesso },
            Retorno = new[]{
                new FornecedorSaida(){
                    Id = 1,
                    Nome = "Nome Fornecedor",
                    Descricao = "Descrição do Fornecedor....",
                    Cnpj = "67090763000141",
                    Ativo = true
                }
            }
        };
    }

    //Consulta
    public class FornecedorExemplo: FornecedorSaida
    {
        public FornecedorSaida FornecedorExemploConsulta() => new FornecedorSaida()
        {
            Id = 1,
            Nome = "Nome Fornecedor",
            Descricao = "Descrição do Fornecedor....",
            Cnpj = "67090763000141",
            Ativo = true
        };
    }

    public class ConsultarFornecedorExemplo : ConsultarSaida
    {
        public ConsultarFornecedorExemplo()
           : base(new FornecedorSaida[] { new FornecedorExemplo { }.FornecedorExemploConsulta() }, FornecedorOrdenarPor.Id.ToString(), "DESC", 1, 1, 5) { }
    }

    public class ConsultarFornecedorResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { FornecedorResource.Fornecedor_Consulta_Realizada_Com_Sucesso },
            Retorno = new ConsultarFornecedorExemplo().Retorno
        };
    }

    //Cadastrar
    public class CadastrarFornecedorRequestExemplo : IExamplesProvider<CadastrarFornecedorViewModel>
    {
        public CadastrarFornecedorViewModel GetExamples() => new CadastrarFornecedorViewModel
        {
            Nome = "Nome do fornecedor",
            Descricao = "Descrição do fornecedor...",
            Cnpj = "67090763000141"
        };
    }

    public class FornecedorSaidaExemplo : FornecedorSaida
    {
        public FornecedorSaida FornecedorExemploCadastro() => new FornecedorSaida()
        {
            Id = 1,
            Nome = "Nome Fornecedor",
            Descricao = "Descrição do Fornecedor....",
            Cnpj = "67090763000141",
            Ativo = true
        };
    }

    public class CadastrarFornecedorResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { FornecedorResource.Fornecedor_Cadastrado_Com_Sucesso},
            Retorno = new FornecedorSaidaExemplo().FornecedorExemploCadastro()
        };
    }

    //Alterar
    public class AlterarFornecedorRequestExemplo : IExamplesProvider<AlterarFornecedorViewModel>
    {
        public AlterarFornecedorViewModel GetExamples() => new AlterarFornecedorViewModel
        {
            Id = 100,
            Nome = "Nome do fornecedor",
            Descricao = "Nova descrição",
            Ativo = true
        };
    }

    public class AlterarFornecedorResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { FornecedorResource.Fornecedor_Alterado_Com_Sucesso },
            Retorno = new[]{
                new FornecedorSaida(){
                    Id = 1,
                    Nome = "Nome Fornecedor",
                    Descricao = "Descrição do Fornecedor....",
                    Cnpj = "67090763000141",
                    Ativo = true
                }
            }
        };
    }

    //Desativar
    public class DesativarFornecedorResponseExemplo : IExamplesProvider<Response>
    {
        public Response GetExamples() => new Response
        {
            Sucesso = true,
            Mensagens = new[] { FornecedorResource.Fornecedor_Excluido_Com_Sucesso },
            Retorno = new[]{
                new FornecedorSaida(){
                    Id = 1,
                    Nome = "Nome Fornecedor",
                    Descricao = "Descrição do Fornecedor....",
                    Cnpj = "67090763000141",
                    Ativo = false
                }
            }
        };
    }
}
