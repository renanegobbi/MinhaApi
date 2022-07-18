using System;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Interfaces.Comandos.Saida;

namespace MinhaApi.Business.Interfaces.Servicos
{
    public interface IProdutoServico
    {
        Task<ProdutoSaida> ObterProdutoPorId(int idProduto);

        Task<ISaida> ObterTodos(ProcurarProdutoEntrada entrada);

        Task<ISaida> ObterProduto(int idProduto);

        Task<ISaida> AdicionarProduto(Produto produto);

        Task<ISaida> AlterarProduto(Produto produto);

        Task<ISaida> DesativarProduto(int id);
    }
}