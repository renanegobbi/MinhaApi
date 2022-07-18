using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Enums;

namespace MinhaApi.Api.ViewModels.Produto
{
    public class ConsultarProdutoViewModel
    {
        /// <summary>
        /// ID do Produto
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// ID do fornecedor
        /// </summary>
        public int? FornecedorId { get; set; }

        /// <summary>
        /// Data de fabricação
        /// </summary>
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime? DataFabricacao { get; set; }

        /// <summary>
        /// Data de validade
        /// </summary>
        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime? DataValidade { get; set; }

        /// <summary>
        /// Status de ativação 
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
        [EnumDataType(typeof(ProdutoOrdenarPor), ErrorMessage = "O tipo de ordenação é inválido")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProdutoOrdenarPor? OrdenarPor { get; set; }

        /// <summary>
        /// Sentido da ordenação que será utilizado ordernar os registros encontrados (ASC para crescente; DESC para decrescente)
        /// </summary>
        public string OrdenarSentido { get; set; }
    }
}