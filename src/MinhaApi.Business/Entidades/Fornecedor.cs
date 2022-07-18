using System.Collections.Generic;
using MinhaApi.Core.DomainObjects;

namespace MinhaApi.Business.Entidades
{
    public class Fornecedor: Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Cnpj { get; set; }

        public bool Ativo { get; set; } = true;

        /* EF Relations */
        public IEnumerable<Produto> Produtos { get; set; }
    }
}
