using API.Models;
using API.Utils;

namespace API.Dtos;

public class LojaDto: Notificador<LojaDto>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string CNPJ { get; set; }
    public override void Validar()
    {
        Contrato
            .IsNotNullOrEmpty(Nome, "nome")
            .IsEmail(Email, "email")
            .IsNotNullOrEmpty(CNPJ, "cpf")
            .IsTrue(CnpjUtils.Validar(CNPJ), nameof(CNPJ), "CNPJ invalido")
            .IsNotNullOrEmpty(Telefone, "telefone")
            .IsTrue(TelefoneUtils.Validar(this.Telefone), nameof(Telefone), "Telefone invalido. Use o formato: XX XXXXX-XXXX");
        base.Validar();
    }

    public LojaDto()
    {

    }

    public LojaDto(Loja loja)
    {
        Nome = loja.Nome;
        Telefone = loja.Telefone;
        Email = loja.Email;
        CNPJ = loja.CNPJ;
    }
}
