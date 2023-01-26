using Microsoft.EntityFrameworkCore;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Enums;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaApi.Data.Repositorios
{
    public class FornecedorRepositorio : Repositorio<Fornecedor>, IFornecedorRepositorio
    {
        public FornecedorRepositorio(MinhaApiContext context) : base(context) { }

        public async Task<Tuple<Fornecedor[], double>> ObterTodos(ProcurarFornecedorEntrada entrada)
        {
            IEnumerable<Fornecedor> registros = Db.Fornecedores.AsNoTracking();

            if (entrada.Id.HasValue)
            {
                registros = registros.Where(f => f.Id == entrada.Id);
            }
            if (!string.IsNullOrEmpty(entrada.Nome))
            {
                registros = registros.Where(f => f.Nome == entrada.Nome);
            }
            if (!string.IsNullOrEmpty(entrada.Descricao))
            {
                registros = registros.Where(f => f.Descricao.ToUpper().Contains(entrada.Descricao.ToUpper()));
            }
            if (!string.IsNullOrEmpty(entrada.Cnpj))
            {
                registros = registros.Where(f => f.Cnpj.ToUpper().Contains(entrada.Cnpj.ToUpper()));
            }
            if (entrada.Ativo.HasValue)
            {
                registros = registros.Where(f => f.Ativo == entrada.Ativo);
            }

            switch (entrada.OrdenarPor)
            {
                case FornecedorOrdenarPor.Nome:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Nome)
                        : registros.OrderBy(p => p.Nome);
                    break;
                case FornecedorOrdenarPor.Descricao:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Descricao)
                        : registros.OrderBy(p => p.Descricao);
                    break;
                case FornecedorOrdenarPor.Cnpj:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Cnpj)
                        : registros.OrderBy(p => p.Cnpj);
                    break;
                case FornecedorOrdenarPor.Ativo:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(p => p.Ativo)
                        : registros.OrderBy(p => p.Ativo);
                    break;
                default:
                    registros = entrada.OrdenarSentido == "DESC"
                        ? registros.OrderByDescending(f => f.Id)
                        : registros.OrderBy(f => f.Id);
                    break;
            }

            var totalRegistros = Convert.ToDouble(registros.Count());

            registros = registros
                .Skip((int)entrada.PaginaTamanho * ((int)entrada.PaginaIndex - 1))
                .Take((int)entrada.PaginaTamanho)
                .ToList();

            if (entrada.Paginar())
            {
                return new Tuple<Fornecedor[], double>(registros.ToArray(), totalRegistros);
            }
            else
            {
                return new Tuple<Fornecedor[], double>(registros.ToArray(), totalRegistros);
            }
        }

        public void DesativarFornecedor(Fornecedor fornecedor)
        {
            fornecedor.Ativo = false;
            DbSet.Update(fornecedor);
        }

        public async Task<bool> ObterPorCnpj(string cnpj)
        {
            var registro = await Db.Fornecedores.AsNoTracking().FirstOrDefaultAsync(f => f.Cnpj == cnpj);

            return registro == null ? false : true;
        }
    }
}

