using Newtonsoft.Json;
using System;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Api.ViewModels.Produto
{
    public class CadastrarProdutoViewModel
    {
        public int FornecedorId { get; set; }

        public string Descricao { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DataFabricacao { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DataValidade { get; set; }
    }
}