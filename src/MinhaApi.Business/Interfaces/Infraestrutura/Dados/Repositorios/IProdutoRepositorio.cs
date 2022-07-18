using System;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios
{
    /// <summary>
    /// Interface que expõe os métodos relacionádos ao repositório sobre a tabela "Produto"
    /// </summary>
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<Tuple<Produto[], double>> ObterTodos(ProcurarProdutoEntrada entrada);

        void DesativarProduto(Produto produto);
    }
}