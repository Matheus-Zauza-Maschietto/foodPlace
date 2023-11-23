using API.Dtos;
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
    public string CNPJ { get; set; }
    public List<Produto> Produtos { get; set; }
    public Endereco Endereco { get; set; }

    public Loja()
    {

    }

    public Loja(LojaDto lojaDto, string donoId)
    {
        DonoId = donoId;
        Nome = lojaDto.Nome;
        Telefone = lojaDto.Telefone;
        Email = lojaDto.Email;
        CNPJ = lojaDto.CNPJ;
    }

    public void Atualizar(LojaDto lojaDto)
    {
        Nome = lojaDto.Nome;
        Telefone = lojaDto.Telefone;
        Email = lojaDto.Email;
        CNPJ = lojaDto.CNPJ;
    }
}
