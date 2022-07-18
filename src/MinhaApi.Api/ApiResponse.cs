using Swashbuckle.AspNetCore.Filters;
using MinhaApi.Business.Comandos.Saida;

namespace MinhaApi.Api
{
    /// <summary>
    /// Response padrão da API para o erro HTTP 400
    /// </summary>
    public class BadRequestApiResponse : Saida, IExamplesProvider<Saida>
    {
        public BadRequestApiResponse()
            : base(false, new[] { "O campo X é obrigatório e não foi informado.", "O campo Y é obrigatório e não foi informado." }, null)
        {
        }

        public Saida GetExamples()
        {
            return new BadRequestApiResponse();
        }
    }
}
