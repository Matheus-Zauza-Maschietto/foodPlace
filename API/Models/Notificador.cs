using API.Models.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;
using System.Diagnostics.Contracts;

namespace API.Models;

public abstract class Notificador<T>: Notifiable<Notification>, INotificador
{
    public readonly Contract<T> Contrato  = new Contract<T>();

    public virtual void Validar()
    {
        AddNotifications(Contrato);
    }

    public Dictionary<string, string[]> GerarNotificacoes()
    {
        var notificacoes = this.Notifications.GroupBy(p => p.Key)
                                             .ToDictionary(p => p.Key, p => p.Select(x => x.Message)
                                                                             .ToArray());
        return notificacoes;

    }
}