﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinhaApi.Data.Context;

namespace MinhaApi.Data.Migrations
{
    [DbContext(typeof(MinhaApiContext))]
    [Migration("20220718144833_AddDb")]
    partial class AddDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("MinhaApi.Business.Entidades.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            Cnpj = "09559340000121",
                            Descricao = "Descrição Fornecedor 1",
                            Nome = "Fornecedor 1"
                        },
                        new
                        {
                            Id = 2,
                            Ativo = true,
                            Cnpj = "21914901000169",
                            Descricao = "Descrição Fornecedor 2",
                            Nome = "Fornecedor 2"
                        },
                        new
                        {
                            Id = 3,
                            Ativo = true,
                            Cnpj = "06974874000126",
                            Descricao = "Descrição Fornecedor 3",
                            Nome = "Fornecedor 3"
                        });
                });

            modelBuilder.Entity("MinhaApi.Business.Entidades.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("FornecedorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("Produtos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Ativo = true,
                            DataFabricacao = new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Local),
                            DataValidade = new DateTime(2022, 7, 28, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "Descrição do produto 1",
                            FornecedorId = 1
                        },
                        new
                        {
                            Id = 2,
                            Ativo = true,
                            DataFabricacao = new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Local),
                            DataValidade = new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "Descrição do produto 2",
                            FornecedorId = 1
                        },
                        new
                        {
                            Id = 3,
                            Ativo = true,
                            DataFabricacao = new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Local),
                            DataValidade = new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Local),
                            Descricao = "Descrição do produto 3",
                            FornecedorId = 1
                        });
                });

            modelBuilder.Entity("MinhaApi.Business.Entidades.Produto", b =>
                {
                    b.HasOne("MinhaApi.Business.Entidades.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("MinhaApi.Business.Entidades.Fornecedor", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
