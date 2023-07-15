using API.Context;
using API.Dtos;
using API.Utils;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Repositories;

public class UsuarioRepositorio
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuarioRepositorio(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityUser> BuscarUsuarioPorEmail(string _email)
    {
        var email = _userManager.FindByEmailAsync(_email).Result;
        return email;
    }

    public async Task<bool> VerificarSenha(IdentityUser usuario, string _senha)
    {

        var senha = _userManager.CheckPasswordAsync(usuario, _senha).Result;
        return senha;
    }

    public async Task<string> GerarToken(string _email)
    {
        var Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, _email),
            });

        var token = new JsonWebToken().GerarToken(Subject, _configuration);
        return token;
    }

    public async Task<bool> CriarUsuario(CadastroDto cadastroDto)
    {
        var user = new IdentityUser
        {
            UserName = cadastroDto.Nome,
            Email = cadastroDto.Email
        };

        var result = _userManager.CreateAsync(user, cadastroDto.Senha).Result;

        if(result.Succeeded) 
        {
            _userManager.AddClaimsAsync(user, _gerarClaims(cadastroDto));
        }

        return result.Succeeded;
    }

    private IEnumerable<Claim> _gerarClaims(CadastroDto cadastroDto)
    {
        var userClaims = new List<Claim>
            {
                new Claim("CPF",  CpfUtils.Formatar(cadastroDto.CPF)),
                new Claim("Telefone", TelefoneUtils.Formatar(cadastroDto.Telefone)),
                new Claim("DataDeNascimento", cadastroDto.DataNascimento.ToString("dd-MM-yyyy")),
                new Claim("Email", cadastroDto.Email),
            };
        return userClaims;
    }
}
