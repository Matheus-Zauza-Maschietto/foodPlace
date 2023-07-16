namespace API.Responses;

public record class LojaResponse(int? Id = null, string Nome = null, string CNPJ = null, string Telefone = null, string Email = null, string Mensagem = "");
