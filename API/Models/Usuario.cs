namespace API.Models;

public class Usuario
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public List<Loja> Lojas { get; set; }
}
