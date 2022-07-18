using System;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios
{
    /// <summary>
    /// Interface que expõe os métodos relacionádos ao repositório sobre a tabela "Fornecedor"
    /// </summary>
    public interface IFornecedorRepositorio : IRepositorio<Fornecedor>
    {
        Task<Tuple<Fornecedor[], double>> ObterTodos(ProcurarFornecedorEntrada entrada);

        Task<bool> ObterPorCnpj(string cnpj);

        void DesativarFornecedor(Fornecedor fornecedor);
    }
}