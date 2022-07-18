using System.ComponentModel;

namespace MinhaApi.Business.Enums
{
    public enum FornecedorOrdenarPor
    {
        [Description("ID do fornecedor")]
        Id,

        [Description("Nome do fornecedor")]
        Nome,

        [Description("Descrição do fornecedor")]
        Descricao,

        [Description("Cnpj")]
        Cnpj,

        [Description("Ativo")]
        Ativo
    }
}


