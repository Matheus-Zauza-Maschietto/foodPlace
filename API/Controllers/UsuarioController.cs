using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepositorio _repositorio;
        public UsuarioController(UsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet()]
        public IActionResult OlaMundo()
        {
            _repositorio.teste();
            return Ok("Olá Mundo");
        }
    }
}
