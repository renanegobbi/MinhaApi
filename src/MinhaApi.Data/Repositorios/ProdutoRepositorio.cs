using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Enums;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Data.Context;

namespace MinhaApi.Data.Repositorios
{
    public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(MinhaApiContext context) : base(context) { }

        public async Task<Tuple<Produto[], double>> ObterTodos(ProcurarProdutoEntrada entrada)
        {
            IEnumerable<Produto> registros = Db.Produtos.AsNoTracking();

            if (entrada.Id.HasValue) 
            {
                registros = registros.Where(p => p.Id == entrada.Id);
            }
            if (entrada.FornecedorId.HasValue)
            {
                registros = registros.Where(p => p.FornecedorId == entrada.FornecedorId);
            }
            if (!string.IsNullOrEmpty(entrada.Descricao))
            {
                registros = registros.Where(p => p.Descricao.ToUpper().Contains(entrada.Descricao.ToUpper()));
            }
            if (entrada.DataFabricacao.HasValue)
            {
                registros = registros.Where(p => p.DataFabricacao.Date == entrada.DataFabricacao?.Date);
            }
            if (entrada.DataValidade.HasValue)
            {
                registros = registros.Where(p => p.DataValidade.Date == entrada.DataValidade?.Date);
            }
            if (entrada.Ativo.HasValue)
            {
                registros = registros.Where(p => p.Ativo == entrada.Ativo);
            }

            switch (entrada.OrdenarPor)
            {
                case ProdutoOrdenarPor.FornecedorId:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.FornecedorId)
                        : registros.OrderBy(p => p.FornecedorId);
                    break;
                case ProdutoOrdenarPor.Descricao:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Descricao)
                        : registros.OrderBy(p => p.Descricao);
                    break;
                case ProdutoOrdenarPor.DataFabricacao:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.DataFabricacao)
                        : registros.OrderBy(p => p.DataFabricacao);
                    break;
                case ProdutoOrdenarPor.DataValidade:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.DataValidade)
                        : registros.OrderBy(p => p.DataValidade);
                    break;
                case ProdutoOrdenarPor.Ativo:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Ativo)
                        : registros.OrderBy(p => p.Ativo);
                    break;
                default:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Id)
                        : registros.OrderBy(p => p.Id);
                    break;
            }

            var totalRegistros = Convert.ToDouble(registros.Count());

            registros = registros
                .Skip((int)entrada.PaginaTamanho * ((int)entrada.PaginaIndex - 1))
                .Take((int)entrada.PaginaTamanho)
                .ToList();

            if (entrada.Paginar())
            {
                return new Tuple<Produto[], double>(registros.ToArray(), totalRegistros);
             }
            else
            {
                return new Tuple<Produto[], double>(registros.ToArray(), totalRegistros);
            }
        }

        public void DesativarProduto(Produto produto)
        {
            produto.Ativo = false;
            DbSet.Update(produto);
        }
    }
}
