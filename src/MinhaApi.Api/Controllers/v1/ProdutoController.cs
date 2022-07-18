using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Net;
using System.Threading.Tasks;
using MinhaApi.Api.Swagger;
using MinhaApi.Api.Swagger.Exemplos;
using MinhaApi.Api.ViewModels.Produto;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Interfaces.Servicos;

namespace MinhaApi.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BadRequestApiResponse), (int)HttpStatusCode.BadRequest)]
    [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(BadRequestApiResponse))]
    [SwaggerTag("Permite a gestão e consulta dos dados de produtos.")]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoServico _produtoServico;
        private readonly IMapper _mapper;

        public ProdutoController(INotificador notificador, 
            IProdutoServico produtoServico,
            IMapper mapper) : base(notificador)
        {
            _produtoServico = produtoServico;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém um produto a partir do seu ID.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/obter-por-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Produto obtido com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ObterProdutoSaidaResponseExemplo))]
        public async Task<IActionResult> ObterProdutoPorId([FromQuery, SwaggerParameter("ID do Produto.", Required = true)] int id)
        {
            try
            {
                return new ApiResult(await _produtoServico.ObterProduto(id));
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Obtém os produtos baseados nos parâmetros de consulta.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/listar")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Consulta realizada com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ConsultarProdutoResponseExemplo))]
        [SwaggerRequestExample(typeof(ConsultarProdutoViewModel), typeof(ConsultarProdutoRequestExemplo))]
        public async Task<IActionResult> ListarProduto(ConsultarProdutoViewModel model)
        {
            try
            {
                var entrada = _mapper.Map<ProcurarProdutoEntrada>(model);

                return new ApiResult(await _produtoServico.ObterTodos(entrada));
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Salva um produto na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/salvar")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Produto cadastrado com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CadastrarProdutoResponseExemplo))]
        [SwaggerRequestExample(typeof(CadastrarProdutoViewModel), typeof(CadastrarProdutoRequestExemplo))]
        public async Task<IActionResult> SalvarProduto(CadastrarProdutoViewModel model)
        {
            try
            {            
                var produto = _mapper.Map<Produto>(model);

                var saida = await _produtoServico.AdicionarProduto(produto);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Altera um produto na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpPut]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/alterar")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Produto alterado com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(AlterarProdutoResponseExemplo))]
        [SwaggerRequestExample(typeof(AlterarProdutoViewModel), typeof(AlterarProdutoRequestExemplo))]
        public async Task<IActionResult> AlterarProduto(AlterarProdutoViewModel model)
        {
            try
            {
                var produtoAnterior = await _produtoServico.ObterProduto(model.Id);

                var produto = _mapper.Map<Produto>(model);

                var saida = await _produtoServico.AlterarProduto(produto);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Exclui um produto na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpDelete]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/excluir")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Produto excluído com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DesativarProdutoResponseExemplo))]
        public async Task<IActionResult> ExcluirProduto([FromQuery, SwaggerParameter("ID do Produto.", Required = true)] int id)
        {
            try
            {
                var saida = await _produtoServico.DesativarProduto(id);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }
    }
}
