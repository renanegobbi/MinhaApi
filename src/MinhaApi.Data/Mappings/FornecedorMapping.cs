using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : N => Fornecedor: Produtos
            // Um fornecedor tem muitos produtos
            builder.HasMany(f => f.Produtos)
                // Um produto tem muitos fornecedores
                .WithOne(p => p.Fornecedor)
                //Chave extrangeira na tabela Produtos é FornecedorId
                .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}