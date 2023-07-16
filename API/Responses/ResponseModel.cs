using API.Models.Interfaces;

namespace API.Responses
{
    public class ResponseModel<T>
    {
        public Dictionary<string, string[]> Erros { get; set; }
        public T Response { get; set; }
        public ResponseModel(INotificador erros, T response)
        {
            Erros = erros.GerarNotificacoes();
            Response = response;
        }
    }
}
