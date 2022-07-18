using System;
using MinhaApi.Business.Notificacoes;
using MinhaApi.Business.Util.Validacoes;

namespace MinhaApi.Business.Comandos.Entrada
{
    public abstract class ConsultarEntrada<TOrdenarPor> : Notificador
    {
        public int? PaginaIndex { get; }

        public int? PaginaTamanho { get; }

        public TOrdenarPor OrdenarPor { get; }

        public string OrdenarSentido { get; }

        protected ConsultarEntrada(TOrdenarPor ordenarPor, string ordenarSentido, int? paginaIndex = null, int? paginaTamanho = null)
        {
            this.OrdenarPor = ordenarPor;
            this.OrdenarSentido = !string.Equals(ordenarSentido, "ASC", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(ordenarSentido, "DESC", StringComparison.InvariantCultureIgnoreCase)
                ? "ASC"
                : ordenarSentido.ToUpper();
            this.PaginaIndex = paginaIndex;
            this.PaginaTamanho = paginaTamanho;

            this.Validar();
        }

        public bool Paginar()
        {
            return this.PaginaIndex.HasValue && this.PaginaTamanho.HasValue;
        }

        protected virtual void Validar()
        {
            if (this.PaginaIndex.HasValue)
                this.NotificarSeMenorQue(this.PaginaIndex.Value, 1, "Index da paginação é inválido.");

            if (this.PaginaTamanho.HasValue)
                this.NotificarSeMenorQue(this.PaginaTamanho.Value, 1, "Tamanho da página utilizado na paginação é inválido.");
        }
    }
}
