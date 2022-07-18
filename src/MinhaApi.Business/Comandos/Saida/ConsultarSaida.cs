using System.Collections.Generic;
using System.Linq;

namespace MinhaApi.Business.Comandos.Saida
{
    public class ConsultarSaida : Saida
    {
        public ConsultarSaida(
            IEnumerable<object> registros,
            string ordenarPor,
            string ordenarSentido,
            double totalRegistros,
            int? paginaIndex = null,
            int? paginaTamanho = null)
            : base(
                true,
                new[] { "Consulta realizada com sucesso." },
                new ConsultarRetorno(paginaIndex, paginaTamanho, ordenarPor, ordenarSentido, totalRegistros, registros))
        {

        }

        public ConsultarSaida(IEnumerable<object> registros)
            : base(
                true,
                new[] { "Consulta realizada com sucesso." },
                new ConsultarRetorno(null, null, null, null, registros != null ? registros.Count() : 0, registros))
        {

        }

        public class ConsultarRetorno
        {
            public int? PaginaIndex { get; }

            public int? PaginaTamanho { get; }

            public string OrdenarPor { get; }

            public string OrdenarSentido { get; }

            public double TotalRegistros { get; }

            public int? TotalPaginas { get; }

            public IEnumerable<object> Registros { get; }

            public ConsultarRetorno(
                int? paginaIndex,
                int? paginaTamanho,
                string ordenarPor,
                string ordenarSentido,
                double totalRegistros,
                IEnumerable<object> registros)
            {
                PaginaIndex = paginaIndex;
                PaginaTamanho = paginaTamanho;
                OrdenarPor = ordenarPor;
                OrdenarSentido = ordenarSentido;
                TotalRegistros = totalRegistros;
                Registros = registros;

                if (paginaTamanho.HasValue)
                {
                    TotalPaginas = totalRegistros % paginaTamanho.Value != 0
                        ? (int)(totalRegistros / paginaTamanho.Value) + 1
                        : (int)(totalRegistros / paginaTamanho.Value);
                }
            }
        }
    }
}
