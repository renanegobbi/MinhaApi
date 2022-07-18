﻿using Newtonsoft.Json;
using System;
using MinhaApi.Business.Entidades;

namespace MinhaApi.Business.Comandos.Saida
{
    public class ProdutoSaida
    {
        public int Id { get; set; }

        public int FornecedorId { get; set; }

        public string Descricao { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DataFabricacao { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd/MM/yyyy")]
        public DateTime DataValidade { get; set; }

        public bool Ativo { get; set; }
    }
}
