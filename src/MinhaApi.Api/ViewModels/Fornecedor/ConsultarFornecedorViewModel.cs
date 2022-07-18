using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using MinhaApi.Business.Enums;

namespace MinhaApi.Api.ViewModels.Fornecedor
{
    public class ConsultarFornecedorViewModel
    {
        /// <summary>
        /// ID do Fornecedor
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Nome do fornecedor
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do fornecedor
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Cnpj do fornecedor
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// Situação do fornecedor
        /// </summary>
        public bool? Ativo { get; set; }

        /// <summary>
        /// Index da página que deseja obter (iniciando por 1)
        /// </summary>
        public int? PaginaIndex { get; set; }

        /// <summary>
        /// Quantidade de registros que deverão ser retornados por página
        /// </summary>
        public int? PaginaTamanho { get; set; }

        /// <summary>
        /// Nome da propriedade que se deseja ordernar os registros encontrados
        /// </summary>
        [EnumDataType(typeof(FornecedorOrdenarPor), ErrorMessage = "O tipo de ordenação é inválido")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FornecedorOrdenarPor? OrdenarPor { get; set; }

        /// <summary>
        /// Sentido da ordenação que será utilizado ordernar os registros encontrados (ASC para crescente; DESC para decrescente)
        /// </summary>
        public string OrdenarSentido { get; set; }
    }
}