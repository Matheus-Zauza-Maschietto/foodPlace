using System.Globalization;

namespace API.Responses;

public record class CadastroResponse(string Email, string Mensagem = "Cadastro criado com sucesso");
