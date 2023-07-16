namespace API.Responses;

public record class ProdutoResponse(Guid? Id = null, string Nome = null, string Descricao = null, bool? Disponivel = null, decimal? Preco = null, string Menssagem = null);