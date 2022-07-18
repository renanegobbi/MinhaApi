using System;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Interfaces.Comandos.Saida;

namespace MinhaApi.Business.Interfaces.Servicos
{
    public interface IFornecedorServico
    {
        Task<bool> ObterFornecedorPorCnpj(string cnpj);

        Task<FornecedorSaida> ObterFornecedorPorId(int idFornecedor);

        Task<ISaida> ObterTodos(ProcurarFornecedorEntrada entrada);

        Task<ISaida> ObterFornecedor(int idFornecedor);

        Task<ISaida> AdicionarFornecedor(Fornecedor fornecedor);

        Task<ISaida> AlterarFornecedor(Fornecedor fornecedor);

        Task<ISaida> DesativarFornecedor(int id);
    }
}
