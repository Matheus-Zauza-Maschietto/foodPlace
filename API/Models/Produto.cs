using API.Dtos;

namespace API.Models;

public class Produto
{
    public Guid Id { get; set; }
    public Loja Loja { get; set; }
    public int LojaId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public bool Disponivel { get; set; } = true;

    public Produto()
    {

    }
    public Produto(ProdutoDto Dto, int lojaId)
    {
        LojaId = lojaId;
        Nome = Dto.Nome;
        Descricao = Dto.Descricao;
        Preco = Dto.Preco;
        Disponivel = Dto.Disponivel;
    }

    public void Atualizar(ProdutoDto produtoDto)
    {
        Nome = produtoDto.Nome;
        Descricao = produtoDto.Descricao;
        Preco = produtoDto.Preco;
        Disponivel = produtoDto.Disponivel;
    }
}
