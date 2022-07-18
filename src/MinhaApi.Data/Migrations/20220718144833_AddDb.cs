using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaApi.Data.Migrations
{
    public partial class AddDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataFabricacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataValidade = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Ativo", "Cnpj", "Descricao", "Nome" },
                values: new object[] { 1, true, "09559340000121", "Descrição Fornecedor 1", "Fornecedor 1" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Ativo", "Cnpj", "Descricao", "Nome" },
                values: new object[] { 2, true, "21914901000169", "Descrição Fornecedor 2", "Fornecedor 2" });

            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Ativo", "Cnpj", "Descricao", "Nome" },
                values: new object[] { 3, true, "06974874000126", "Descrição Fornecedor 3", "Fornecedor 3" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "DataFabricacao", "DataValidade", "Descricao", "FornecedorId" },
                values: new object[] { 1, true, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), "Descrição do produto 1", 1 });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "DataFabricacao", "DataValidade", "Descricao", "FornecedorId" },
                values: new object[] { 2, true, new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Local), "Descrição do produto 2", 1 });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "DataFabricacao", "DataValidade", "Descricao", "FornecedorId" },
                values: new object[] { 3, true, new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 6, 0, 0, 0, 0, DateTimeKind.Local), "Descrição do produto 3", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_FornecedorId",
                table: "Produtos",
                column: "FornecedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
