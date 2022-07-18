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
using MinhaApi.Api.ViewModels.Fornecedor;
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
    [SwaggerTag("Permite a gestão e consulta dos dados de fornecedores.")]
    public class FornecedorController : BaseController
    {
        private readonly IFornecedorServico _fornecedorServico;
        private readonly IMapper _mapper;

        public FornecedorController(INotificador notificador,
            IFornecedorServico fornecedorServico,
            IMapper mapper) : base(notificador)
        {
            _fornecedorServico = fornecedorServico;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém um fornecedor a partir do seu ID.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/obter-por-id")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Fornecedor obtido com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ObterFornecedorSaidaResponseExemplo))]
        public async Task<IActionResult> ObterFornecedorPorId([FromQuery, SwaggerParameter("ID do Fornecedor.", Required = true)] int id)
        {
            try
            {
                return new ApiResult(await _fornecedorServico.ObterFornecedor(id));
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Obtém os fornecedores baseados nos parâmetros de consulta.
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
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ConsultarFornecedorResponseExemplo))]
        [SwaggerRequestExample(typeof(ConsultarFornecedorViewModel), typeof(ConsultarFornecedorRequestExemplo))]
        public async Task<IActionResult> ListarFornecedor(ConsultarFornecedorViewModel model)
        {
            try
            {
                var entrada = _mapper.Map<ProcurarFornecedorEntrada>(model);

                return new ApiResult(await _fornecedorServico.ObterTodos(entrada));
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Salva um fornecedor na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/salvar")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Fornecedor cadastrado com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(CadastrarFornecedorResponseExemplo))]
        [SwaggerRequestExample(typeof(CadastrarFornecedorViewModel), typeof(CadastrarFornecedorRequestExemplo))]
        public async Task<IActionResult> SalvarFornecedor(CadastrarFornecedorViewModel model)
        {
            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(model);

                var saida = await _fornecedorServico.AdicionarFornecedor(fornecedor);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Altera um fornecedor na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpPut]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/alterar")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Fornecedor alterado com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(AlterarFornecedorResponseExemplo))]
        [SwaggerRequestExample(typeof(AlterarFornecedorViewModel), typeof(AlterarFornecedorRequestExemplo))]
        public async Task<IActionResult> AlterarFornecedor(AlterarFornecedorViewModel model)
        {
            try
            {
                var fornecedorAnterior = await _fornecedorServico.ObterFornecedor(model.Id);

                if(fornecedorAnterior.Sucesso == false) { return new ApiResult(fornecedorAnterior); }

                var fornecedor = _mapper.Map<Fornecedor>(model);

                var saida = await _fornecedorServico.AlterarFornecedor(fornecedor);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }

        /// <summary>
        /// Exclui um fornecedor na base de dados.
        /// </summary>
        /// <remarks>Observações:
        /// <ul>
        ///     <li>Para acessar essa rota, não é necessário estar logado no sistema.</li>
        /// </ul>
        /// </remarks>
        [HttpDelete]
        [AllowAnonymous]
        [Route("v{version:apiVersion}/[controller]/excluir")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Fornecedor excluído com sucesso.", typeof(Response))]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DesativarFornecedorResponseExemplo))]
        public async Task<IActionResult> ExcluirFornecedor([FromQuery, SwaggerParameter("ID do Fornecedor.", Required = true)] int id)
        {
            try
            {
                var saida = await _fornecedorServico.DesativarFornecedor(id);

                return new ApiResult(saida);
            }
            catch (Exception ex)
            {
                return new ApiResult(new Saida(false, new string[] { ex?.Message }, null));
            }
        }
    }
}
