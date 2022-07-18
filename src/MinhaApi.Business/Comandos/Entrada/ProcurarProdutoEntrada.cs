using System;
using MinhaApi.Business.Enums;
using MinhaApi.Business.Interfaces.Comandos.Entrada;

namespace MinhaApi.Business.Comandos.Entrada
{
    public class ProcurarProdutoEntrada : ConsultarEntrada<ProdutoOrdenarPor>, IEntrada
    {
        public int? Id { get; set; }

        public int? FornecedorId { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataFabricacao { get; set; }

        public DateTime? DataValidade { get; set; }

        public bool? Ativo { get; set; }

        public ProcurarProdutoEntrada(ProdutoOrdenarPor? ordenarPor = ProdutoOrdenarPor.Id, string ordenarSentido = "ASC", int? paginaIndex = null, int? paginaTamanho = null)
            : base(ordenarPor.Value, ordenarSentido, paginaIndex, paginaTamanho)
        {
        }
    }
}
