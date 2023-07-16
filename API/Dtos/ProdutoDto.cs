using API.Models;

namespace API.Dtos;

public class ProdutoDto: Notificador<ProdutoDto>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; }

    public override void Validar()
    {
        Contrato
            .IsNotNullOrEmpty(Nome, "Nome")
            .IsNotNull(Preco, "Preço")
            .IsGreaterOrEqualsThan(Preco, 0, "Preço")
            .IsNotNull(Disponivel, "Disponivel");

        base.Validar();
    }
}
