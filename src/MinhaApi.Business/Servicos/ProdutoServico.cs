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
    public class ProdutoServico : BaseServico, IProdutoServico
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<ProdutoServico> _logger;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio,
                              IFornecedorRepositorio fornecedorRepositorio,
                              INotificador notificador,
                              IMapper mapper,
                              ILogger<ProdutoServico> logger,
                              IUnitOfWork uow) : base(uow)
        {
            _produtoRepositorio = produtoRepositorio;
            _fornecedorRepositorio = fornecedorRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProdutoSaida> ObterProdutoPorId(int idProduto)
        {
            var produto = await _produtoRepositorio.ObterPorId(idProduto);

            if (produto == null) return null;

            var produtoSaida = _mapper.Map<ProdutoSaida>(produto);

            return produtoSaida;
        }

        public async Task<ISaida> ObterProduto(int idProduto)
        {
            var saida = await ObterProdutoPorId(idProduto);

            return saida == null
                ? new Saida(false, new[] { ProdutoResource.Nenhum_Produto_Encontrado }, null)
                : new Saida(true, new[] { ProdutoResource.Produto_Obtido_Com_Sucesso }, saida);
        }

        public async Task<ISaida> ObterTodos(ProcurarProdutoEntrada entrada)
        {
            this.NotificarSeNulo(entrada, ProdutoResource.Entrada_Nao_Informada);

            if (this.Invalido)
                return new Saida(false, this.Mensagens, null);

            this.AdicionarNotificacoes(entrada.Notificacoes);

            if (this.Invalido)
                return new Saida(false, this.Mensagens, null);

            var resultado = await _produtoRepositorio.ObterTodos(entrada);

            return new ConsultarSaida(resultado.Item1.Select(x => _mapper.Map<ProdutoSaida>(x)), entrada.OrdenarPor.ToString(), entrada.OrdenarSentido, resultado.Item2, entrada.PaginaIndex, entrada.PaginaTamanho);
        }

        public async Task<ISaida> AdicionarProduto(Produto produto)
        {
            this.NotificarSeNulo(produto, ProdutoResource.Entrada_Nao_Informada);

            NotificarErrorValidation(new ProdutoValidation(), produto);

            var fornecedor = await _fornecedorRepositorio.ObterPorId(produto.FornecedorId);

            if (fornecedor == null)
                this.NotificarSeNulo(fornecedor, "ID do fornecedor não encontrado.");

            if (TemNotificacao())
                return new Saida(false, ObterNotificacoes().Select(a => a.Mensagem).ToList(), null);

            try
            {
                await _produtoRepositorio.Adicionar(produto);

                _uow.CommitTransaction();

                var produtoSaida = await ObterProdutoPorId(produto.Id);

                _logger.LogInformation($"O produto \"{produtoSaida.Id} - {produtoSaida.Descricao}\" foi cadastrado.");

                return new Saida(true, new[] { ProdutoResource.Produto_Cadastrado_Com_Sucesso }, produtoSaida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao cadastrar o produto {produto.Descricao}: {ex.Message}");
                throw;
            }
        }

        public async Task<ISaida> AlterarProduto(Produto produto)
        {
            this.NotificarSeNulo(produto, ProdutoResource.Entrada_Nao_Informada);

            NotificarErrorValidation(new ProdutoValidation(), produto);

            var fornecedor = await _fornecedorRepositorio.ObterPorId(produto.FornecedorId);

            if (fornecedor == null)
                this.NotificarSeNulo(fornecedor, FornecedorResource.Fornecedor_ID_Nao_Encontrado);

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            var produtoAntes = await ObterProdutoPorId(produto.Id);
            if (produtoAntes == null) return new Saida(false, new[] { ProdutoResource.Nenhum_Produto_Encontrado }, null);

            try
            {
                _produtoRepositorio.Atualizar(produto);

                _uow.CommitTransaction();

                var produtoSaida = await ObterProdutoPorId(produto.Id);

                _logger.LogInformation($"O produto \"{produtoAntes.Id} - {produtoAntes.Descricao}\" foi alterado.");

                return new Saida(true, new[] { ProdutoResource.Produto_Alterado_Com_Sucesso }, produtoSaida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao alterar o produto \"{produto.Id} - {produto.Descricao}\": {ex.Message}");
                throw;
            }
        }

        public async Task<ISaida> DesativarProduto(int id)
        {
            this.NotificarSeMenorQue(id, 1, "Id inválido.");

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            var produtoAntes = await ObterProdutoPorId(id);

            if (produtoAntes == null) return new Saida(false, new[] { ProdutoResource.Nenhum_Produto_Encontrado }, null);

            if (Invalido)
                return new Saida(false, this.Mensagens, null);

            try
            {
                var produto = _mapper.Map<Produto>(produtoAntes);

                _produtoRepositorio.DesativarProduto(produto);

                _uow.CommitTransaction();

                var produtoSaida = await ObterProdutoPorId(id);

                _logger.LogInformation($"O produto \"{produtoAntes.Id} - {produtoAntes.Descricao}\" foi desativado.");

                return new Saida(true, new[] { ProdutoResource.Produto_Excluido_Com_Sucesso }, produtoSaida);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao desativar o produto {id}: {ex.Message}");
                throw;
            }
        }
    }
}

