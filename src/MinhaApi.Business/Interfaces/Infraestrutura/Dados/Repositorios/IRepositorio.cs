using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MinhaApi.Core.DomainObjects;

namespace MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios
{
    public interface IRepositorio<TEntity> where TEntity : Entity
    {
        Task<TEntity> ObterPorId(int id);
        Task<List<TEntity>> ObterTodos();
        Task Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(TEntity entity);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    }
}