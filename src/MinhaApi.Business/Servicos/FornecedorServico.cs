using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.Business.Comandos.Entrada;
using MinhaApi.Business.Comandos.Saida;
using MinhaApi.Business.Entidades;
using MinhaApi.Business.Entidades.Validations;
using MinhaApi.Business.Interfaces;
using MinhaApi.Business.Interfaces.Comandos.Saida;
using MinhaApi.Business.Interfaces.Infraestrutura.Dados.Repositorios;
using MinhaApi.Business.Interfaces.Servicos;
using MinhaApi.Business.Resources;
using MinhaApi.Business.Util.Validacoes;
using MinhaApi.Core.Data;

namespace MinhaApi.Business.Servicos
{
    public class FornecedorServico : BaseServico, IFornecedorServico
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<FornecedorServico> _logger;


        public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio,
                              INotificador notificador,
                              IMapper mapper,
                              ILogger<FornecedorServico> logger,
                              IUnitOfWork uow): base(uow)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<bool> ObterFornecedorPorCnpj(string cnpj)
        {
            bool cnpjExistente = await _fornecedorRepositorio.ObterPorCnpj(cnpj);

            return cnpjExistente;
        }

        public async Task<FornecedorSaida> ObterFornecedorPorId(int idFornecedor)
        {
            var fornecedor = await _fornecedorRepositorio.ObterPorId(idFornecedor);

            if (fornecedor == null) return null;

            var fornecedorSaida = _mapper.Map<FornecedorSaida>(fornecedor);

            return fornecedorSaida;
        }

        public async Task<ISaida> ObterFornecedor(int idFornecedor)
        {
            var saida = await ObterFornecedorPorId(idFornecedor);

            return saida == null
                ? new Saida(false, new[] { FornecedorResource.Nenhum_Fornecedor_Encontrado }, null)
                : new Saida(true, new[] { FornecedorResource.Fornecedor_Obtido_Com_Sucesso }, saida);
        }

        public async Task<ISaida> ObterTodos(ProcurarFornecedorEntrada entrada)
        {
            this.NotificarSeNulo(entrada, FornecedorResource.Entrada_Nao_Informada);

            if (this.Invalido)
                return new Saida(false, this.Mensagens, null);

            this.AdicionarNotificacoes(entrada.Notificacoes);

            if (this.Invalido)
                return new Saida(false, this.Mensagens, null);

            var resultado = await _fornecedorRepositorio.ObterTodos(entrada);

            return new ConsultarSaida(resultado.Item1.Select(x => _mapper.Map<FornecedorSaida>(x)), entrada.OrdenarPor.ToString(), entrada.OrdenarSentido, resultado.Item2, entrada.PaginaIndex, entrada.PaginaTamanho);
        }

        public async Task<ISaida> AdicionarFornecedor(Fornecedor fornecedor)
        {
            this.NotificarSeNulo(fornecedor, FornecedorResource.Entrada_Nao_Informada);

            NotificarErrorValidation(new FornecedorValidation(), fornecedor);

            if (TemNotificacao())
                return new Saida(false, ObterNotificacoes().Select(a => a.Mensagem).ToList(), null);

            var cnpjExistente = await ObterFornecedorPorCnpj(fornecedor.Cnpj);

            if (cnpjExistente == true)
                return new Saida(false, new[] { FornecedorResource.Fornecedor_Cnpj_Ja_Existente }, null);

            try 
            { 
                await _fornecedorRepositorio.Adicionar(fornecedor);

                _uow.CommitTransaction();

                var fornecedorSaida = await ObterFornecedorPorId(fornecedor.Id);

                _logger.LogInformation($"O fornecedor \"{fornecedorSaida.Id} - {fornecedorSaida.Nome}\" foi cadastrado.");

                return new Saida(true, new[] { FornecedorResource.Fornecedor_Cadastrado_Com_Sucesso }, fornecedorSaida);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao cadastrar o fornecedor {fornecedor.Nome}: {ex.Message}");
                throw;
            }
        }

        public async Task<ISaida> AlterarFornecedor(Fornecedor fornecedor)
        {
            this.NotificarSeNulo(fornecedor, FornecedorResource.Entrada_Nao_Informada);

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            var fornecedorAntes = await ObterFornecedorPorId(fornecedor.Id);

            fornecedor.Cnpj = fornecedorAntes.Cnpj;

            NotificarErrorValidation(new FornecedorValidation(), fornecedor);

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            if (fornecedorAntes == null) return new Saida(false, new[] { FornecedorResource.Nenhum_Fornecedor_Encontrado }, null);

            try
            {
                _fornecedorRepositorio.Atualizar(fornecedor);

                _uow.CommitTransaction();

                var fornecedorSaida = await ObterFornecedorPorId(fornecedor.Id);

                _logger.LogInformation($"O fornecedor \"{fornecedorAntes.Id} - {fornecedorAntes.Nome}\" foi alterado.");

                return new Saida(true, new[] { FornecedorResource.Fornecedor_Alterado_Com_Sucesso }, fornecedorSaida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao alterar o fornecedor \"{fornecedor.Id} - {fornecedor.Nome}\": {ex.Message}");
                throw;
            }
        }

        public async Task<ISaida> DesativarFornecedor(int id)
        {
            this.NotificarSeMenorQue(id, 1, "Id inválido.");

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            var fornecedorAntes = await ObterFornecedorPorId(id);

            if (fornecedorAntes == null) return new Saida(false, new[] { FornecedorResource.Nenhum_Fornecedor_Encontrado }, null);

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorAntes);

                _fornecedorRepositorio.DesativarFornecedor(fornecedor);

                _uow.CommitTransaction();

                var fornecedorSaida = await ObterFornecedorPorId(id);

                _logger.LogInformation($"O fornecedor \"{fornecedorAntes.Id} - {fornecedorAntes.Nome}\" foi desativado.");

                return new Saida(true, new[] { FornecedorResource.Fornecedor_Excluido_Com_Sucesso }, fornecedorSaida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao desativar o fornecedor {id}: {ex.Message}");
                throw;
            }
        }
    }
}
