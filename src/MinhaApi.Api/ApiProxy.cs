namespace MinhaApi.Api
{
    public class ApiProxy
    {
        private readonly string _urlApi;

        public ApiProxy(string ambiente)
        {
            switch (ambiente)
            {
                case "DES":
                    _urlApi = "https://localhost:5001/api/minhaapi";
                    break;
                default:
                    break;
            }
        }

        public string ObterUrlApi() => _urlApi;
    }
}
