namespace API.Models;

public class ProdutoCategoria
{
    public Categoria Categoria { get; set; }
    public int CategoriaId { get; set; }
    public Produto Produto { get; set; }
    public Guid ProdutoId { get; set; }
}
