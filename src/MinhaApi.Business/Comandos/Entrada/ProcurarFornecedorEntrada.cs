using MinhaApi.Business.Enums;
using MinhaApi.Business.Interfaces.Comandos.Entrada;

namespace MinhaApi.Business.Comandos.Entrada
{
    public class ProcurarFornecedorEntrada : ConsultarEntrada<FornecedorOrdenarPor>, IEntrada
    {
        public int? Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Cnpj { get; set; }

        public bool? Ativo { get; set; }

        public ProcurarFornecedorEntrada(FornecedorOrdenarPor? ordenarPor = FornecedorOrdenarPor.Id, string ordenarSentido = "ASC", int? paginaIndex = null, int? paginaTamanho = null)
            : base(ordenarPor.Value, ordenarSentido, paginaIndex, paginaTamanho)
        {
        }
    }
}
