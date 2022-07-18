using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MinhaApi.Business.Entidades;
using MinhaApi.Data.Extensions;
using MinhaApi.Data.Mappings;

namespace MinhaApi.Data.Context
{
    public class MinhaApiContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public MinhaApiContext(DbContextOptions<MinhaApiContext> options): base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtension.AdicionaFornecedores(modelBuilder);
            ModelBuilderExtension.AdicionaProdutos(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                var prop = property.GetColumnType();
                prop = "varchar(100)";
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinhaApiContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Produto>(new ProdutoMapping());
            modelBuilder.ApplyConfiguration<Fornecedor>(new FornecedorMapping());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
