using System.ComponentModel;

namespace MinhaApi.Business.Enums
{
    public enum ProdutoOrdenarPor
    {
        [Description("ID do produto")]
        Id,

        [Description("ID da fornecedor")]
        FornecedorId,

        [Description("Descrição do produto")]
        Descricao,

        [Description("Ativo")]
        Ativo,

        [Description("Data de fabricação")]
        DataFabricacao,

        [Description("Data de validade")]
        DataValidade
    }
}


