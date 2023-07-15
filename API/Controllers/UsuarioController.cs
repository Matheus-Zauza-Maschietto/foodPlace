using API.Dtos;
using API.Repositories;
using API.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    [AllowAnonymous]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _repositorio;
        public UsuarioController(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginDto loginDto)
        {
            loginDto.Validar();
            if (!loginDto.IsValid)
            {
                return Results.BadRequest(loginDto.GerarNotificacoes());
            }

            IdentityUser usuario = await _repositorio.BuscarUsuarioPorEmail(loginDto.Email);

            if (usuario is null)
            {
                loginDto.AddNotification("email", "email not found");
                return Results.NotFound(loginDto.GerarNotificacoes());
            }

            if (!await _repositorio.VerificarSenha(usuario, loginDto.Senha))
            {
                loginDto.AddNotification("senha", "incorrect senha");
                return Results.NotFound(loginDto.GerarNotificacoes());
            }

            string token = await _repositorio.GerarToken(loginDto.Email);
            return Results.Ok(new LoginResponse(token));
        }

        [HttpPost("cadastro")]
        public async Task<IResult> Cadastro(CadastroDto cadastroDto)
        {
            cadastroDto.Validar();
            if (!cadastroDto.IsValid)
            {
                return Results.BadRequest(cadastroDto.GerarNotificacoes());
            }

            IdentityUser usuario = await _repositorio.BuscarUsuarioPorEmail(cadastroDto.Email);
            if (usuario is not null)
            {
                cadastroDto.AddNotification("email", "email already exists at another account");
                return Results.BadRequest(cadastroDto.GerarNotificacoes());
            }

            if (await _repositorio.CriarUsuario(cadastroDto))
                return Results.Created("/login", new CadastroResponse(cadastroDto.Email));
            else
                return Results.UnprocessableEntity();
        }
    }
}
