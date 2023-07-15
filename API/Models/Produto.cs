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
    public List<ProdutoCategoria> Categorias { get; set; }
}
