using Microsoft.EntityFrameworkCore;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Core.DomainObjects;
using MinhaApi.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MinhaApi.Data.Repositorios
{
    public abstract class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : Entity, new()
    {
        protected readonly MinhaApiContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repositorio(MinhaApiContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(int id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Adicionar(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Remover(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}