using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class Loja
{
    public int Id { get; set; }
    public IdentityUser Dono { get; set; }
    public string DonoId { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public Endereco Endereco { get; set; }
    public int EnderecoId { get;}
    public string CNPJ { get; set; }
    public List<Produto> Produtos { get; set; }
}
