using API.Models;
using API.Utils;
namespace API.Dtos;

public class CadastroDto: Notificador<CadastroDto>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Telefone { get; set; }
    public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }

    public override void Validar()
    {
        Contrato
            .IsNotNullOrEmpty(Nome.Trim(), "nome")
            .IsEmail(Email, "email")
            .IsNotNullOrEmpty(CPF, "cpf")
            .IsFalse(CpfUtils.Validar(CPF), nameof(CPF), "CPF invalido")
            .IsNotNullOrEmpty(Telefone, "telefone")
            .IsFalse(TelefoneUtils.Validar(this.Telefone), nameof(Telefone), "Telefone invalido. Use o formato: XX XXXXX-XXXX")
            .IsGreaterOrEqualsThan(Senha, 8, "senha")
            .IsNotNull(DataNascimento, "data de nascimento");
        base.Validar();
    }
}
