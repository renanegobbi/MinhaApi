using AutoMapper;
using MinhaApi.Api.ViewModels.Fornecedor;
using MinhaApi.Api.ViewModels.Produto;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            //Produto
            CreateMap<Produto, ProdutoSaida>();
            CreateMap<CadastrarProdutoViewModel, Produto>();
            CreateMap<AlterarProdutoViewModel, Produto>();
            CreateMap<ConsultarProdutoViewModel, ProcurarProdutoEntrada>()
                .ConstructUsing(x => new ProcurarProdutoEntrada(x.OrdenarPor, x.OrdenarSentido, x.PaginaIndex, x.PaginaTamanho));
            CreateMap<ProdutoSaida, Produto>()
                .ForMember(Produto => Produto.Ativo, opt => opt.MapFrom(ProdutoSaida => ProdutoSaida.Ativo == false));

            //Fornecedor
            CreateMap<Fornecedor, FornecedorSaida>();
            CreateMap<CadastrarFornecedorViewModel, Fornecedor>();
            CreateMap<AlterarFornecedorViewModel, Fornecedor>();
            CreateMap<ConsultarFornecedorViewModel, ProcurarFornecedorEntrada>()
                .ConstructUsing(x => new ProcurarFornecedorEntrada(x.OrdenarPor, x.OrdenarSentido, x.PaginaIndex, x.PaginaTamanho));
            CreateMap<FornecedorSaida, Fornecedor>()
                .ForMember(Fornecedor => Fornecedor.Ativo, opt => opt.MapFrom(FornecedorSaida => FornecedorSaida.Ativo == false));
        }
    }
}