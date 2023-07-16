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
            ResponseModel<LoginResponse> response;
            loginDto.Validar();

            if (!loginDto.IsValid)
            {
                response = new ResponseModel<LoginResponse>(loginDto, null);
                return Results.BadRequest(response);
            }

            IdentityUser usuario = await _repositorio.BuscarUsuarioPorEmail(loginDto.Email);
            if (usuario is null)
            {
                loginDto.AddNotification("email", "email not found");
                response = new ResponseModel<LoginResponse>(loginDto, null);
                return Results.NotFound(response);
            }

            if (!await _repositorio.VerificarSenha(usuario, loginDto.Senha))
            {
                loginDto.AddNotification("senha", "incorrect senha");
                response = new ResponseModel<LoginResponse>(loginDto, null);
                return Results.NotFound(response);
            }

            string token = await _repositorio.GerarToken(loginDto.Email);
            response = new ResponseModel<LoginResponse>(loginDto, new LoginResponse(token));

            return Results.Ok(response);
        }

        [HttpPost("cadastro")]
        public async Task<IResult> Cadastro(CadastroDto cadastroDto)
        {
            ResponseModel<CadastroResponse> response;
            cadastroDto.Validar();

            if (!cadastroDto.IsValid)
            {
                response = new ResponseModel<CadastroResponse>(cadastroDto, null);
                return Results.BadRequest(response);
            }

            IdentityUser usuario = await _repositorio.BuscarUsuarioPorEmail(cadastroDto.Email);
            if (usuario is not null)
            {
                cadastroDto.AddNotification("email", "email already exists at another account");
                response = new ResponseModel<CadastroResponse>(cadastroDto, null);
                return Results.BadRequest(response);
            }

            if (await _repositorio.CriarUsuario(cadastroDto))
            {
                response = new ResponseModel<CadastroResponse>(cadastroDto, new CadastroResponse(cadastroDto.Email));
                return Results.Created("/login", response);
            }
            else
            {
                response = new ResponseModel<CadastroResponse>(cadastroDto, null);
                return Results.UnprocessableEntity(response);
            }

        }
    }
}
