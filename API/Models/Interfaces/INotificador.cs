namespace API.Models.Interfaces
{
    public interface INotificador
    {
        void Validar();
        Dictionary<string, string[]> GerarNotificacoes();
    }
}
