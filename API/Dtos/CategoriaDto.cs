using API.Models;

namespace API.Dtos;

public class CategoriaDto: Notificador<CategoriaDto>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public override void Validar()
    {
        Contrato
            .IsNotNullOrEmpty(Nome, "Nome")
            .IsNotNullOrEmpty(Descricao, "Descricao");

        base.Validar();
    }
}
