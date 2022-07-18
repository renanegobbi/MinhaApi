using Microsoft.EntityFrameworkCore;
using System;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder AdicionaFornecedores(this ModelBuilder builder)
        {
            builder.Entity<Fornecedor>().HasData(

               new Fornecedor
               {
                   Id = 1,
                   Nome = "Fornecedor 1",
                   Descricao = "Descrição Fornecedor 1",
                   Cnpj = "09559340000121",
                   Ativo = true
               },
               new Fornecedor
               {
                   Id = 2,
                   Nome = "Fornecedor 2",
                   Descricao = "Descrição Fornecedor 2",
                   Cnpj = "21914901000169",
                   Ativo = true
               },
               new Fornecedor
               {
                   Id = 3,
                   Nome = "Fornecedor 3",
                   Descricao = "Descrição Fornecedor 3",
                   Cnpj = "06974874000126",
                   Ativo = true
               }
            );
            
            return builder;
        }

        public static ModelBuilder AdicionaProdutos(this ModelBuilder builder)
        {
            builder.Entity<Produto>().HasData(

               new Produto
               {
                   Id = 1,
                   FornecedorId = 1,
                   Descricao = "Descrição do produto 1",
                   DataFabricacao = DateTime.Now.Date,
                   DataValidade = DateTime.Now.Date.AddDays(10),
                   Ativo = true
               },
               new Produto
               {
                   Id = 2,
                   FornecedorId = 1,
                   Descricao = "Descrição do produto 2",
                   DataFabricacao = DateTime.Now.Date.AddDays(11),
                   DataValidade = DateTime.Now.Date.AddDays(30),
                   Ativo = true
               },
               new Produto
               {
                   Id = 3,
                   FornecedorId = 1,
                   Descricao = "Descrição do produto 3",
                   DataFabricacao = DateTime.Now.Date.AddDays(21),
                   DataValidade = DateTime.Now.Date.AddDays(50),
                   Ativo = true
               }
            );

            return builder;
        }
    }
}

