using System;
using MinhaApi.Core.DomainObjects;

namespace MinhaApi.Business.Entidades
{
    public class Produto: Entity
    {
        public int FornecedorId { get; set; }

        public string Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataFabricacao { get; set; }

        public DateTime DataValidade { get; set; }

        /* EF Relations */
        public Fornecedor Fornecedor { get; set; }
    }
}